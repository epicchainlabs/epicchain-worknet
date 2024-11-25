using EpicChain;
using EpicChain.Network.P2P.Payloads;
using EpicChain.Network.RPC.Models;
using EpicChain.SmartContract.Manifest;
using EpicChain.VM;
using EpicChain.Wallets;
using NeoShell.Models;

namespace NeoShell
{
  interface INode : IDisposable
  {
    ProtocolSettings ProtocolSettings { get; }

    enum CheckpointMode { Online, Offline }

    Task<RpcInvokeResult> InvokeAsync(Script script, Signer? signer = null);

    Task<UInt256> ExecuteAsync(Wallet wallet, UInt160 accountHash, WitnessScope witnessScope, Script script, decimal additionalGas = 0);

    Task<ContractManifest> GetContractAsync(UInt160 scriptHash);
    
    Task<IReadOnlyList<(UInt160 hash, ContractManifest manifest)>> ListContractsAsync();

    Task<IReadOnlyList<TokenContract>> ListTokenContractsAsync();

    Task<Block> GetLatestBlockAsync();

    Task<Block> GetBlockAsync(UInt256 blockHash);

    Task<Block> GetBlockAsync(uint blockIndex);

    Task<(Transaction tx, RpcApplicationLog? appLog)> GetTransactionAsync(UInt256 txHash);

  }
}
