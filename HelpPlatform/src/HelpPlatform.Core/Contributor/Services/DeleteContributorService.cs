﻿using Ardalis.Result;
using Ardalis.SharedKernel;
using HelpPlatform.Core.Contributor.ContributorAggregate.Events;
using HelpPlatform.Core.Contributor.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HelpPlatform.Core.Contributor.Services;

/// <summary>
/// This is here mainly so there's an example of a domain service
/// and also to demonstrate how to fire domain events from a service.
/// </summary>
/// <param name="_repository"></param>
/// <param name="_mediator"></param>
/// <param name="_logger"></param>
public class DeleteContributorService(
    IRepository<ContributorAggregate.Contributor> _repository,
    IMediator _mediator,
    ILogger<DeleteContributorService> _logger) : IDeleteContributorService {
    public async Task<Result> DeleteContributor(int contributorId) {
        _logger.LogInformation("Deleting Contributor {contributorId}", contributorId);
        ContributorAggregate.Contributor? aggregateToDelete = await _repository.GetByIdAsync(contributorId);
        if (aggregateToDelete == null) return Result.NotFound();

        await _repository.DeleteAsync(aggregateToDelete);
        var domainEvent = new ContributorDeletedEvent(contributorId);
        await _mediator.Publish(domainEvent);
        return Result.Success();
    }
}
