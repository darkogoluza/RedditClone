# Introduction
This project covers 3 things: Web API (C#), Blazor (C#) and Entity Framework Core (C#). Goal of this project is to recreate the famous social media [Reddit](https://www.reddit.com). This was an assignment from university and I was restricted to only using the .net environment and only using the 3 mentioned technologies above. The project is 3 tiers and it was divided into 2 different parts. First part is only focused on the first two tiers and simplifies the 3th tier. 3th tier is made using only JSON (Java Script Object Notation) and it is saved only to a file. This approach of course is not the optimal one because there will be a lot of repeating data. Second part focused on replacing the 3th tier with EFC (Entity Framework Core) and querying using LINQ (Language-Integrated Query).  

![Clean Onion Arhitecture](https://cdn.sanity.io/images/trvpzega/production/09b5ade6468775bba7c98bfd6a64e825ecec7e4a-1332x994.png)
Image above is called "Onion Architecture" because it is circular shaped with layers inside. The idea is that the layers closer to the center should change less. This system uses this architecture and allowed the system to be modular, for example it was easy to swap out databases to EFC (Entity Framework Core) without changing any other layers. That was possible because the database was living more on the outside layers.

# Requirements
## Minimum
1. Registering
2. Login in/out
3. Creating a new post (when logged in)
4. Post must contain a title and a body (recemented a new page for this) 
5. Viewing all posts.
6. Clicking on the post to show overview (recemented a new page for this) where you can view entire post.
## Additional 
1. Commenting and sub commenting
2. Each post has a sub-forum that belongs to
# Domain model 
The figure below shows the domain model. The domain model is quite simple and it outlines only 4 entities. The "User" entity can only contain "userName" and "password" and other 3 entities depend on it. "subForum" has a "User" who created it and a "type". "Post" belongs to a "subForum" and has a "User" who created the "Post". "Comment" is the most complex It can both be commented on a "Post" and as well on another "Comment". "Comment" also has a reference to a "User" who has created it.

![](https://cdn.sanity.io/images/trvpzega/production/8a64132655709c346242d3d88d4c332ddae97b42-1280x1287.jpg)
# Implementation

Starting with the business logic, and creating the Web API. First I have created all of the endpoints and then moved my way down to the logic layer. Instance of a create endpoint for posts 
```csharp:PostController.cs
  [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync(PostCreationDto postCreationDto)
    {
        try
        {
            Post post = await postLogic.CreateAsync(postCreationDto);
            return Created($"/posts/{post.Id}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        } 
    }
```
We can see that the method only calls `postLogic.CreateAsync()` method and the rest is just returning a response. `postLogic` is injected in the constructor 
```csharp:PostController.cs
 private readonly IPostLogic postLogic;

    public PostsController(IPostLogic postLogic)
    {
        this.postLogic = postLogic;
    }
```
and it comes from logic layer.

Second thing is to define interfaces Between the Web API and the logic implementation. They serve a purpose to satisfy `Dependency inversion principle`, one of the principals of `SOLID`. It helps us loosely couple different components of the system. The  `IPostLogic` interfaces looks like this
```csharp:IPostLogic.cs
public interface IPostLogic  
{  
    Task<Post> CreateAsync(PostCreationDto postCreationDto);  
    Task<IEnumerable<Post>> GetAsync(string? subForm);  
    Task<Post?> GetByIdAsync(int id);  
    Task UpdateAsync(PostUpdateDto postUpdateDto);  
    Task DeleteAsync(int id);  
}
```
We can see that the interface contains mostly CRUD (Create, Read, Update and Delete) methods. 

Moving on to the implementation we validate the information and then pass it on to DAO.
```csharp:PostLogic.cs
public async Task<Post> CreateAsync(PostCreationDto postCreationDto)  
{  
    SubForum? subForum = await subForumDao.GetByIdAsync(postCreationDto.BelongsToId);  
    if(subForum == null)  
        throw new Exception($"Sub forum with id {postCreationDto.BelongsToId} does not exist");  
      
      
    User? user = await userDao.GetByIdAsync(postCreationDto.OwnerId);  
    if (user == null)  
        throw new Exception($"User with id {postCreationDto.OwnerId} does not exist");  
  
    Post post = new Post(user, subForum, postCreationDto.Title, postCreationDto.Body);  
  
    return await postDao.CreateAsync(post);  
}
```
Again the DAO is injected in the constructor.
## Blazor
For creating a post on the client side, there is a separate page with a form. Upon a request to create a post `HttpClient` is called. The `HttpClient` is injected in to the page 
```razor:CreatePost.razor
@inject IPostService postService
```
The `HttpClient` handles communication with the Web API.
```csharp:PostHttpClient.cs
    public async Task CreateAsync(string title, string body, int selectedSubFormId, int ownerId)
    {
        PostCreationDto postCreationDto = new(title, body, selectedSubFormId, ownerId);

        string subFormAsJson = JsonSerializer.Serialize(postCreationDto);
        StringContent content = new(subFormAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("https://localhost:7207/Posts", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
```
Data is first serialized to `JSON` and send to the end point. Response is caught and handled.
# Result
Video example can be found on the link bellow

[![Youtube video](https://img.youtube.com/vi/DsxmKawHeq4/0.jpg)](https://www.youtube.com/watch?v=DsxmKawHeq4)

