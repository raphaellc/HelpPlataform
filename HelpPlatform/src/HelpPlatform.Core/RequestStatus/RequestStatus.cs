using System.Runtime.CompilerServices;
using Ardalis.SmartEnum;

namespace HelpPlatform.Core.RequestStatus;

public abstract class RequestStatus : SmartEnum<RequestStatus> {
    public static readonly RequestStatus Open = new OpenStatus();

    public static readonly RequestStatus PartiallyClaimed = new PartiallyClaimedStatus();

    public static readonly RequestStatus Claimed = new ClaimedStatus();

    public static readonly RequestStatus Completed = new CompletedStatus();

    public static readonly RequestStatus Cancelled = new CancelledStatus();

    public abstract bool CanTransitionTo(RequestStatus next);

    protected RequestStatus(String name, int value) : base(name,value){}

    private sealed class OpenStatus: RequestStatus
    {
        public OpenStatus() : base("Open", 0) {}

        public override bool CanTransitionTo(RequestStatus next) =>
            next == RequestStatus.PartiallyClaimed || next == RequestStatus.Claimed || next == RequestStatus.Cancelled;
    }

    private sealed class PartiallyClaimedStatus : RequestStatus
    {
        public PartiallyClaimedStatus() : base("PartiallyClaimed", 1) {}

        public override bool CanTransitionTo(RequestStatus next) =>
            next == RequestStatus.Claimed || next == RequestStatus.Cancelled;
    }

    private sealed class ClaimedStatus : RequestStatus
    {
        public ClaimedStatus() : base("Claimed", 2) {}

        public override bool CanTransitionTo(RequestStatus next) =>
            next == RequestStatus.Completed || next == RequestStatus.Cancelled;
    }

    private sealed class CompletedStatus : RequestStatus
    {
        public CompletedStatus() : base("Completed", 3) {}

        public override bool CanTransitionTo(RequestStatus next) =>
            false;
    }

    private sealed class CancelledStatus : RequestStatus
    {
        public CancelledStatus() : base("Cancelled", 3) {}

        public override bool CanTransitionTo(RequestStatus next) =>
            false;
    }


}
