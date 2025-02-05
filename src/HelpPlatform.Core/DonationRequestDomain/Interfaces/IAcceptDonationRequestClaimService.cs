﻿using Ardalis.Result;

namespace HelpPlatform.Core.DonationRequestDomain.Interfaces;

public interface IAcceptDonationRequestClaimService
{
    public Task<Result> AcceptClaim(int requestId, int claimId, CancellationToken cancellationToken);
}
