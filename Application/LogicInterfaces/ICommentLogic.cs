﻿using Domain;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface ICommentLogic
{
    Task<Comment> CreateAsync(CommentCreationDto creationDto);
    Task<IEnumerable<Comment>> GetAsync();
    Task<Comment?> GetByIdAsync(int id);
    Task UpdateAsync(CommentUpdateDto commentUpdateDto);
    Task DeleteAsync(int id);
}