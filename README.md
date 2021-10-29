# data-processing
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
