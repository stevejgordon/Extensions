﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Threading;
using Microsoft.Shared.Diagnostics;
using Polly;

namespace Microsoft.Extensions.Resilience.Options;

#pragma warning disable SA1649 // File name should match first type name

/// <summary>
/// Structure with the arguments of the on retry action.
/// </summary>
/// <typeparam name="TResult">The type of the result handled by the policy.</typeparam>
#pragma warning disable CA1815 // Override equals and operator equals on value types (Such usage is not expected in this scenario)
public readonly struct RetryActionArguments<TResult> : IPolicyEventArguments<TResult>
#pragma warning restore CA1815
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RetryActionArguments{TResult}" /> structure.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="context">The policy context.</param>
    /// <param name="waitingTimeInterval">The waiting time interval.</param>
    /// <param name="attemptNumber">The attempt number.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public RetryActionArguments(
        DelegateResult<TResult> result,
        Context context,
        TimeSpan waitingTimeInterval,
        int attemptNumber,
        CancellationToken cancellationToken)
    {
        _ = Throw.IfLessThan(waitingTimeInterval.Ticks, 0);
        WaitingTimeInterval = waitingTimeInterval;
        Result = Throw.IfNull(result);
        Context = Throw.IfNull(context);
        AttemptNumber = Throw.IfLessThan(attemptNumber, 0);
        CancellationToken = cancellationToken;
    }

    /// <summary>
    /// Gets the result of the action executed by the retry policy.
    /// </summary>
    public DelegateResult<TResult> Result { get; }

    /// <summary>
    /// Gets the waiting time interval.
    /// </summary>
    public TimeSpan WaitingTimeInterval { get; }

    /// <summary>
    /// Gets the attempt number.
    /// </summary>
    public int AttemptNumber { get; }

    /// <summary>
    /// Gets the Polly <see cref="global::Polly.Context" /> associated with the policy execution.
    /// </summary>
    public Context Context { get; }

    /// <summary>
    /// Gets the cancellation token associated with the policy execution.
    /// </summary>
    public CancellationToken CancellationToken { get; }
}