#####################################################			
# Pawel Zielinski
#####################################################	

# The shortest path problem in a directed graph G=<V,A>

using JuMP 
using GLPKMathProgInterface # pakiet GLPK



struct Arc
	i::Int       # arc (i,j)
	j::Int
	c::Float64   # cij  the cost of arc (i,j) 
end	

#Data
n=4
s=1 # the source
t=n # the sink
A = [Arc(1,2,1.0), Arc(1,3,4.0), Arc(2,3,2.0), Arc(2,4,8.0), Arc(3,4,5.0)] # The network





# Building model for the shortest path problem.
model = Model(solver = GLPKSolverLP()) # choosing LP GLPKSolver
@variable(model, x[A]>=0) # x[i,j] =1 if  arc belongs to the shortest path, 0 otherwise
@objective(model,Min, sum(a.c * x[a] for a in A)) # the objective function

V=1:n # the set of nodes

@constraint(model, sum(x[a] for a in A if a.j==t) == 1)
@constraint(model, sum(x[a] for a in A if a.i==s) == 1)
for k=filter(v->v!=s && v!=t,V)
  @constraint(model, sum(x[a] for a in A if a.j==k) == sum(x[a] for a in A if a.i==k))
end


print(model) # print the instance of problem

status = solve(model, suppress_warnings=true) # solve model



if status==:Optimal
    println("the total cost: ", getobjectivevalue(model))
    result = getvalue(x)
    for r in result.indexsets[1]
        if result[r] > 0
            print(result[r])
            print("\t")
            println(r)
        end
    end
    
else
   println("Status: ", status)
end




