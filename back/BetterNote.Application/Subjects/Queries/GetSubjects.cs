﻿using MediatR;

namespace BetterNote.Application.Subjects.Queries;
public record GetSubjects(int? Skip, int? Take, string? SortBy, string? SortDirection) : IRequest<SubjectResultSet>;
