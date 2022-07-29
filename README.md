# Azure Dapr for .NET Developers Part 2
This is the repository for the LinkedIn Learning course Azure Dapr for .NET Developers Part 2. The full course is available from [LinkedIn Learning][lil-course-url].

![Azure Dapr for .NET Developers Part 2][lil-thumbnail-url] 

Are you ready to take your new skills in Dapr to the next level for successful deployment? In this course, the second in a two-part series, instructor Rodrigo Díaz Concha shows you how to manage, configure, and deploy distributed applications as microservices in Kubernetes and Azure Container Apps. Explore some of the most important building blocks of a resilient, Dapr-engineered solution, including best practices for secrets management, configuration, and end-to-end observability. Learn how to extend and apply Dapr components to be deployed as fully containerized, testable microservices in a Kubernetes cluster or in Azure Container Apps. Along the way, Rodrigo shares key advice to help you get the most out of your Dapr experience.

## Instructions
This repository has branches for each of the videos in the course. You can use the branch pop up menu in github to switch to a specific branch and take a look at the course at that stage, or you can add `/tree/BRANCH_NAME` to the URL to go to the branch you want to access.

## Branches
The branches are structured to correspond to the videos in the course. The naming convention is `CHAPTER#_MOVIE#`. As an example, the branch named `02_03` corresponds to the second chapter and the third video in that chapter. 
Some branches will have a beginning and an end state. These are marked with the letters `b` for "beginning" and `e` for "end". The `b` branch contains the code as it is at the beginning of the movie. The `e` branch contains the code as it is at the end of the movie. The `main` branch holds the final state of the code when in the course.

When switching from one exercise files branch to the next after making changes to the files, you may get a message like this:

    error: Your local changes to the following files would be overwritten by checkout:        [files]
    Please commit your changes or stash them before you switch branches.
    Aborting

To resolve this issue:
	
    Add changes to git using this command: git add .
	Commit changes using this command: git commit -m "some message"


### Instructor

Rodrigo Díaz Concha 
                            
                            

Check out my other courses on [LinkedIn Learning](https://www.linkedin.com/learning/instructors/rodrigo-diaz-concha).

[lil-course-url]: https://www.linkedin.com/learning/azure-dapr-for-dot-net-developers-part-2
[lil-thumbnail-url]: https://cdn.lynda.com/course/2472000/2472000-1658941731588-16x9.jpg
