﻿using System;
using System.Collections.Generic;
using System.Numerics;
using Sequence.Core;
using Sequence.Core.Signature;
using Sequence.Core.Wallet;

namespace Sequence.Core.V2.Signature.Tree
{
    public class SignatureTreeDynamicSignatureLeaf : ISignatureTree
    {
        private enum DynamicSignatureType : byte
        {
            DynamicSignatureTypeEIP712 = 1,
            DynamicSignatureTypeEthSign = 2,
            DynamicSignatureTypeEIP1271 = 3
        }

        public int weight;
        public string address;
        private DynamicSignatureType type;
        public byte[] signature;

        public (IWalletConfigTree, BigInteger) Recover(WalletContext context, Subdigest subdigest, RPCProvider provider, List<SignerSignatures> signerSignatures)
        {
            throw new NotImplementedException();
        }
    }
}
