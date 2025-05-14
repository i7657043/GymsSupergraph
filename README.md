### Gym Supergraph Testing

* 2 .Net Core 8.0 Web APIs both covered by GraphQL using Hochocolate.ApolloFederation

* Combining both Gym and Managers subgraphs into a Supergraph

#### How to run

* Make sure you have npm installed so you can download the latest wcg cli tool required to compose a router file
  
* Run the run.ps1 file at the root of the repo to run the supergraph router, combining both subgraphs into 1 gateway and then query on http://localhost:3002
