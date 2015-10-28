﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System.Management.Automation;
using Microsoft.Azure.Commands.Network.Models;

namespace Microsoft.Azure.Commands.Network
{
    using System.Linq;

    [Cmdlet(VerbsCommon.New, "AzureRmExpressRouteCircuitPeeringConfig"), OutputType(typeof(PSPeering))]
    public class NewAzureExpressRouteCircuitPeeringConfigCommand : AzureExpressRouteCircuitPeeringConfigBase
    {
        [Parameter(
            Mandatory = true,
            HelpMessage = "The name of the Peering")]
        [ValidateNotNullOrEmpty]
        public override string Name { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var peering = new PSPeering();

            peering.Name = this.Name;
            peering.PeeringType = this.PeeringType;
            peering.PrimaryPeerAddressPrefix = this.PrimaryPeerAddressPrefix;
            peering.SecondaryPeerAddressPrefix = this.SecondaryPeerAddressPrefix;
            peering.PeerASN = this.PeerASN;
            peering.VlanId = this.VlanId;

            if (this.MircosoftConfigAdvertisedPublicPrefixes != null
                && this.MircosoftConfigAdvertisedPublicPrefixes.Any())
            {
                peering.MicrosoftPeeringConfig = new PSPeeringConfig();
                peering.MicrosoftPeeringConfig.AdvertisedPublicPrefixes = this.MircosoftConfigAdvertisedPublicPrefixes;
                peering.MicrosoftPeeringConfig.CustomerASN = this.MircosoftConfigCustomerAsn;
                peering.MicrosoftPeeringConfig.RoutingRegistryName = this.MircosoftConfigRoutingRegistryName;
            }

            WriteObject(peering);
        }
    }
}
