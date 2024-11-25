using EpicChain.BlockchainToolkit.Models;

namespace EpicChainWorknet;

interface IWalletWriter : IDisposable
{
    void WriteWallet(ToolkitWallet wallet);

    public static IWalletWriter Create(TextWriter writer, bool json)
    {
        return json
            ? new JsonWalletWriter(writer)
            : new TextWalletWriter(writer);
    }
}