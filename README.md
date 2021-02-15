# Management accounting system
Based on Graph Theory implementation of Cost Graph algorithm in calculation direct, production and general business costs.
## Bussiness process in BPM notations
![image](https://github.com/danmaxdanilov/bi_finance/blob/master/bi_finance.png?raw=true)
## Description
Main calculation in Cost Graph algorithm is calculation of system of linear algebraic equations, which is build from graph by using "Finding shortest path" algorithm.
To simplify calculation of shortest path between Profit Center ("Outlet") and low-level Cost Center ("Source") used Neo4j database engine.
To solve system of linear algebraic equations used matrix equation written through MathNet library. 
Result of solving matrix was written to Neo4j and after, based on stored graph, was reported to SQL Database.
## External sources
1. Explanation of Cost Graph algorithm by Alexander Polyakov on his youtube channel. (https://www.youtube.com/channel/UCtTxARHnTjuawXwoiShvPDQ)
1. https://en.wikipedia.org/wiki/System_of_linear_equations
1. https://transportgeography.org/contents/methods/graph-theory-measures-indices/cost-graph/
