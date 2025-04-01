﻿using BuildingLink.DriverManagement.Application.Shared;

namespace BuildingLink.DriverManagement.Application.Drivers.GetAlphabetizedCollection;

public sealed class GetAlphabetizedCollectionQueryResponse : Result<IReadOnlyList<DriverResult>, GetAlphabetizedCollectionQueryResponse>;