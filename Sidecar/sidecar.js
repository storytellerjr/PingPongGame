
const RPC = require('@hyperswarm/rpc');
const net = require('net');
const os = require('os');

const pipeName = process.platform === 'win32' ? '\\\\.\\pipe\\UnityP2PPipe' : '/tmp/UnityP2PPipe';
const server = net.createServer();
const sockets = [];

async function main() {
  const rpc = new RPC();
  const rpcServer = rpc.createServer();
  await rpcServer.listen();
  console.log('RPC server listening on public key:', rpcServer.publicKey.toString('hex'));

  // Connect to peer (replace with actual peer public key)
  const peerPublicKey = Buffer.from('<PEER_PUBLIC_KEY>', 'hex'); // Update with opponent's key
  const client = rpc.connect(peerPublicKey);

  server.listen(pipeName, () => {
    console.log('Named pipe server listening on', pipeName);
  });

  server.on('connection', (socket) => {
    console.log('Unity connected to sidecar');
    sockets.push(socket);

    socket.on('data', async (data) => {
      const message = data.toString('utf8');
      console.log('Received from Unity:', message);

      // Broadcast to peer
      try {
        await client.request('updateGameState', Buffer.from(message));
      } catch (e) {
        console.error('RPC error:', e.message);
      }
    });

    socket.on('end', () => {
      console.log('Unity disconnected');
      sockets.splice(sockets.indexOf(socket), 1);
    });

    socket.on('error', (e) => {
      console.error('Socket error:', e.message);
    });
  });

  // Handle RPC requests from peer
  rpcServer.on('request', (req, res) => {
    if (req.method === 'updateGameState') {
      const message = req.rawData.toString('utf8');
      console.log('Received from peer:', message);

      // Forward to Unity
      sockets.forEach((socket) => {
        if (socket.writable) {
          socket.write(Buffer.from(message));
        }
      });

      res.end(Buffer.from('OK'));
    }
  });
}

main().catch((e) => console.error('Main error:', e.message));