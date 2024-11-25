<!-- markdownlint-enable -->
# EpicChain-Worknet Command Reference

The `neo-worknet` tool enables a developer to create and run a Neo N3 consensus node that branches
from a public Neo N3 blockchain - including the official Neo N3 MainNet and T5 TestNet. This provides
the developer a local scratchpad environment that mirrors the state of a known public network at a 
specified index. Changes to the local branch of the network are independent of the public network.

> Note, you can pass -?|-h|--help to show a list of supported commands or to show
> help information about a specific command.

## epicchain-worknet create

```
Create a Neo Worknet branch

Usage: epicchain-worknet create [options] <RpcUri> <Output>

Arguments:
  RpcUri              URL of EpicChain JSON-RPC Node
                      Specify MainNet, TestNet or JSON-RPC URL
  Output              Name of .epicchain-worknet file to create (Default: ./default.neo-worknet)

Options:
  -i|--index <INDEX>  Block height to branch at
                      Default value is: 0.
  -f|--force          Overwrite existing data
  --disable-log       Disable verbose data logging
  -?|-h|--help        Show help information.
```

The `create` command creates a new local WorkNet blockchain as a branch from a public Neo N3 blockchain. 
This command will create both a `.neo-worknet` file to hold details about the blockchain branch and a 
`data` folder that will contain data loaded from the remote blockchain and cached locally as well as 
locally generated blocks and contract storage updates.

The user must specify a remote Neo N3 blockchain network to branch from. EpicChain-Worknet has built in knowledge
of MainNet and the T5 TestNet. However, the user can specify any Neo N3 RPC API node they wish. The 
user can specify a specific block index to branch at. If unspecified, `neo-worknet` will branch at the
current height of the specified blockchain. 

> Note, EpicChain-Worknet depends on the StateService and RpcServer [plugins](https://github.com/neo-project/neo-modules)
> to be installed on the `RpcUri` argument. Furthermore, the StateService *MUST* be configured with 
> `FullState` as `true`.

The branched blockchain *CANNOT* be validated across the branch point. When a Neo Worknet branch network
is created, a new wallet account is created to act as the consensus block signer. The public network's
council members' accounts are obviously not available for signing new blocks on a local branch of the
chain. Changing the consensus account that signs blocks requires an update to the `NextConsensus` field.
Updating this field requires adding an *unsigned* block to the local blockchain branch. Since this branch
transition block is unsigned, the blockchain history can not be validated across this transition block. 

Unlike EpicChain-Express, EpicChain-Worknet doesn't provide an option for creating a multiple consensus nodes for
the branched chain. Based on understanding of EpicChain-Express usage patterns, multiple conesnsus nodes are
not typically used. If four or seven conesnsus node support in EpicChain-Worknet is important to you, please
file an issue in our [GitHub repo](https://github.com/neo-project/neo-express/issues)

## epicchain-worknet prefetch

```
Fetch data for specified contract

Usage: epicchain-worknet prefetch [options] <Contract>

Arguments:
  Contract       Name or Hash of contract to prefetch contract storage

Options:
  --disable-log  Disable verbose data logging
  -?|-h|--help   Show help information.
  --input        Path to .epicchain-worknet data file
```

EpicChain-Worknet caches deployed contract storage on first access. For deployed contracts with thousands
of storage records, this can be very time consuming. The `prefetch` command provides a mechanism to
download contract storage before running the chain. This will ensure all data associated with the specified 
contract is downloaded and available so the WorkNet node can run that contract without needing to pause
and download data the first time it's run locally. 

## EpicChain-Worknet reset

```
Reset WorkNet back to initial branch point

Usage: EpicChain-Worknet reset [options]

Options:
  -f|--force    Overwrite existing data
  -?|-h|--help  Show help information.
  --input       Path to .EpicChain-Worknet data file
```

This command resets all the locally generated blocks in the chain. The unsigned branch transition block
(described in the `create` command section) is deleted and regenerated as part of this process.

Any contract data from the public chain that has been cached locally - either via `prefetch` or thru
the normal process of executing transactions on the branched chain - are not affected. Even after a
`reset`, contract storage does not need to be `prefetch`ed again.

## v-worknet run

```
Run EpicChain-Worknet instance node

Usage: epicchain-worknet run [options]

Options:
  -s|--seconds-per-block <SECONDS_PER_BLOCK>  Time between blocks
  -?|-h|--help                                Show help information.
  --input                                     Path to .epicchain-worknet data file
```

Runs the branched blockchain locally. New blocks will be added to the chain every 15 seconds unless
overridded with the `--seconds-per-block` option. 

These new blocks added to the chain have *no* correlation to the blocks added to the public chain that
was branched from. From the point of the branch, the original source chain and the local branched chain
are independent. 

RPC methods that are exposed by EpicChain-Express.

## epicchain-worknet wallet

The wallet command has a series of subcommands for the management of standard wallets and accounts for use in the EpicChain-Worknet blockchain network.

> As with EpicChain-Express wallets, EpicChain-Worknet wallet accounts are stored unencrypted and should never be used in a production context.

```
Manage epicchain-worknet wallets

Usage: epicchain-worknet wallet [command] [options]

Options:
  -?|-h|--help  Show help information.

Commands:
  create        Create epicchain-express wallet
  delete        Delete epicchain-express wallet
  list          List epicchain-express wallets
```

