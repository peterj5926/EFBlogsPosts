using EFBlogsPosts.Models;
using Helper;
using Microsoft.Identity.Client;

namespace EFBlogsPosts
{
    public class Program
    {
        static void Main(string[] args)
        {
            int input;
            bool isComplete = false;
            Console.WriteLine("Program starting");
            do
            {
                
                Console.WriteLine();
                Console.WriteLine("Blogs and Posts Menu options:");
                Console.WriteLine("1. Display Blogs");
                Console.WriteLine("2. Add Blog");
                Console.WriteLine("3. Display Posts");
                Console.WriteLine("4. Add Post");
                Console.WriteLine("5. Exit");

                input = Input.GetIntWithPrompt("Please select an option 1-5: ", "Please try again: ");           

                do
                {
                    if (input > 5 || input < 1)
                    {
                        Console.WriteLine("Please select 1-5 from the menu");
                        input = Input.GetIntWithPrompt("What option would you like: ", "Please try again: ");
                    }
                } while (input > 5 || input < 1);

                if (input == 1)
                {
                    using (var context = new BlogContext())
                    {
                        var blogslist = context.Blogs.ToList();
                        if (blogslist.Count == 0)
                        {
                            Console.WriteLine($"There are {blogslist.Count()} Blogs to List.");  // The last assignment I added alot of extra code for singular and plural responses
                        }                                                                        // I didn't get to involved in it this time because this was something new and tried 
                        else                                                                     // to just focus on the funtion because these are some pretty cool new things we're learning
                        {
                            Console.WriteLine($"{blogslist.Count()} Blogs Returned");
                            Console.WriteLine("The Blogs are:");
                            foreach (var blog in blogslist)
                            {
                                Console.WriteLine($"    {blog.Name}");
                            }
                        }
                    }
                }
                else if (input == 2)
                {
                    using (var context = new BlogContext())
                    {
                        string blogname = Input.GetStringWithPrompt("Enter a blog name: ", "The Blog name can not be blank, Please try again: ");
                        var blog = new Blog();
                        blog.Name = blogname;

                                context.Blogs.Add(blog);
                                context.SaveChanges();
                    }

                }
                else if (input == 3)
                {
                    
                    using (var context = new BlogContext())
                    {
                        var blogslist = context.Blogs.ToList();                       //Using .ToList() seems fine and works for this assignment because we are using such small tables but from 
                                                                                      // from the discussion from class about memory, I feel another way would be better for searching or
                        if (blogslist.Count == 0)                                     // validating things from a database. What if there was a million blogs and millions of post? I know we talked
                        {                                                             // about the "or default" options for nulls and such but its entity framework is still new and a little fuzzy so I
                            Console.WriteLine($"There are {blogslist.Count()} Blogs to list any post from.");// just went with what worked for the assignment.
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Select the Blog that you'd like to view the post from.");
                            foreach (var blog in blogslist)
                            {
                                Console.WriteLine($"{blog.BlogId}: {blog.Name}");
                            }
                            Console.WriteLine();
                            int blogSelection = Input.GetIntWithPrompt("Which Blog Id would you like to view the posts from?: ", "Invalid Blog Id,Please enter the Blog Id number: ");
                            Console.WriteLine();
                            if (blogslist.Any(b => b.BlogId == blogSelection))
                            {
                                
                                
                                var postslist = context.Posts.Where(b => b.BlogId == blogSelection).ToList();
                                if (postslist.Count == 0)
                                {
                                    Console.WriteLine($"There are {postslist.Count()} Posts for that blog.");
                                }
                                else
                                {
                                    Console.WriteLine($"There are {postslist.Count()} Posts for this blog.");
                                    foreach (var post in postslist)
                                    {
                                        Console.WriteLine($"Blog Name: {post.Blog.Name}");
                                        Console.WriteLine($"    {post.Title}");
                                        Console.WriteLine($"    {post.Content}");
                                        Console.WriteLine();
                                    }
                                }
                                
                            }
                            else
                            {
                                Console.WriteLine("There are no Blogs saved with that Id");
                            }


                        }
                    }
                }
                else if (input == 4)
                {

                    using (var context = new BlogContext())
                    {                                                                                // You told us not to pay attention to the pdf example included with the assignment but I noticed that 
                        var blogslist = context.Blogs.ToList();                                      // user errors were treated by just booting the user back to the main menu. It made me start to weigh some
                                                                                                     // of the pros and cons of my helper input methods because I'm forcing the user to enter the correct info
                        if (blogslist.Count == 0)                                                    // without an escape option. The assignment didn't specify how to handle the user errors but my overthinking
                        {                                                                            // has me pondering a modification to them.
                            Console.WriteLine($"There are {blogslist.Count()} Blogs to Post to.");
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Selection of Blogs that you can add a post to.");
                            foreach (var blog in blogslist)
                            {
                                Console.WriteLine($"{blog.BlogId}: {blog.Name}");
                            }
                            Console.WriteLine();
                            int blogSelection = Input.GetIntWithPrompt("Which Blog Id would you like to add a Post to?: ", "Please enter the Blog Id: ");
                            if (blogslist.Any(b => b.BlogId == blogSelection))
                            {
                                string title = Input.GetStringWithPrompt("Enter a post title: ", "The title can not be blank, Please try again: ");
                                string content = Input.GetStringWithPrompt("Enter post content: ","The post content can not be empty, Please try again: ");
                                var post = new Post();
                                post.Title = title;
                                post.Content = content;
                                post.BlogId =  blogSelection;

                                context.Posts.Add(post);
                                context.SaveChanges();


                            }
                            else
                            {
                                Console.WriteLine("There are no Blogs saved with that Id");
                            }


                        }
                    }
                }
                else if (input == 5)
                {
                    isComplete = true;
                }

               
            } while (!isComplete);
       
        }

    }
}

//trash can
//context.Dispose();



//    using (var context = new BlogContext())
//    {
//Console.WriteLine("Enter a post title");
//var title = Console.ReadLine();
//Console.WriteLine("Enter post content");
//var content = Console.ReadLine();
//        Console.WriteLine("Which blog?");
//        var blogId = Console.ReadLine();

//var post = new Post();
//post.Title = title;
//post.Content = content;
//post.BlogId = Convert.ToInt32(blogId);

//context.Posts.Add(post);
//context.SaveChanges();
//    }
//    //read the post
//using (var context = new BlogContext())
//{
//    var postslist = context.Posts.ToList();//.tolist() works with lazyloading

//    Console.WriteLine("The posts are:");
//    foreach (var post in postslist)
//    {
//        Console.WriteLine($"Blog Name: {post.Blog.Name}");
//        Console.WriteLine($"    {post.Title}");
//    }
//}
//}
////Insert a blog
//static void AddBlog()
//{
//    using (var context = new BlogContext())
//    {
//        Console.WriteLine("Enter a blog name: ");
//        var blogname = Console.ReadLine();
//        var blog = new Blog();
//        blog.Name = blogname;

//        context.Blogs.Add(blog);
//        context.SaveChanges();
//    }
//}

//static void ReadBlog()
//{
//    //read
//    using (var context = new BlogContext())
//    {
//        var blogslist = context.Blogs.ToList();
//        Console.WriteLine("The Blogs are:");
//        foreach (var blog in blogslist)
//        {
//            Console.WriteLine($"    {blog.Name}");
//        }
//    }
//}

//static void UpdateBlog()
//{
//    //update
//    using (var context = new BlogContext())
//    {
//        var blogToUpdate = context.Blogs.Where(b => b.BlogId == 1).First();

//        Console.WriteLine($"Your choice is {blogToUpdate.Name}");
//        Console.WriteLine("What do you want the name to be?");
//        var updatedName = Console.ReadLine();

//        blogToUpdate.Name = updatedName;
//        context.SaveChanges();
//    }
//    // remove same as update except use context.remove
//context.Remove(blogToRemove)