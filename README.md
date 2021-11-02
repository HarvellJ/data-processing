[![Board Status](https://dev.azure.com/joshuaharvey55/10aaf9c4-0834-4a92-b7f3-ebe8454e5b91/9a93ed02-fadb-4641-b1ad-f4e81f26c118/_apis/work/boardbadge/3d095655-8565-4659-b272-22ceea84b3d4)](https://dev.azure.com/joshuaharvey55/10aaf9c4-0834-4a92-b7f3-ebe8454e5b91/_boards/board/t/9a93ed02-fadb-4641-b1ad-f4e81f26c118/Microsoft.RequirementCategory)
# Data Processing Pipeline Reference
Note - this is still a work in progress.

This repo contains the source for infrastructure and code required to create a simple data-processing pipeline in Azure. It contains the following components:
* .NET 5.0 API 
* Azure Service Bus Topics with a standard and express subscription
* Azure Functions that process the messages from the topics
* Cosmos DB for storing orders

It can be visualised as the following:

![DataPipelineDiagram](https://user-images.githubusercontent.com/40071640/139442520-89d99b5b-8243-40b2-8776-c558e0327a4c.PNG)

## Uses
The point of this repo is to form a basic end-to-end pipeline using Azure Service Bus, Functions and Cosmos DB that can be used to experiment on with development and monitoring (it will be referenced in multiple posts on my blog)

It currently requires some manual work to deploy, but all of the necessary code to get it working is contained here.
