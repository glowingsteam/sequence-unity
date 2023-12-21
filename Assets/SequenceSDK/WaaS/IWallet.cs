using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sequence.WaaS.Authentication;

namespace Sequence.WaaS
{   
    public interface IWallet
    {
        public Address GetWalletAddress();
        public event Action<SignMessageReturn> OnSignMessageComplete;
        public Task<SignMessageReturn> SignMessage(SignMessageArgs args);

        public Task<IsValidMessageSignatureReturn> IsValidMessageSignature(IsValidMessageSignatureArgs args);
        public event Action<SuccessfulTransactionReturn> OnSendTransactionComplete;
        public event Action<FailedTransactionReturn> OnSendTransactionFailed;
        public Task<TransactionReturn> SendTransaction(SendTransactionArgs args);
        public event Action<ContractDeploymentResult> OnDeployContractComplete;
        public Task<ContractDeploymentResult> DeployContract(Chain network, string bytecode, string value = "0");
        public event Action<string> OnDropSessionComplete;
        public Task<bool> DropSession(string dropSessionId);
        public Task<bool> DropThisSession();
        public event Action<WaaSSession[]> OnSessionsFound;
        public Task<WaaSSession[]> ListSessions();
    }
    
    public class ContractDeploymentResult
    {
        public TransactionReturn TransactionReturn;
        public Address DeployedContractAddress;

        public ContractDeploymentResult(TransactionReturn transactionReturn, Address deployedContractAddress)
        {
            TransactionReturn = transactionReturn;
            DeployedContractAddress = deployedContractAddress;
        }
    }
}