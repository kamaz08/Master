using JuMP 
using GLPKMathProgInterface 

function singleMachine(p::Vector{Int}, r::Vector{Int}, w::Vector{Float64})

    n=length(p)
    #  n - liczba zadan
    #  p - wektor czasow wykonania zadan
    #  r - wektor momentow dostepnosci zadan

    T= maximum(r)+sum(p)+1 
    
    model = Model(solver = GLPKSolverMIP())

    
    Task = 1:n
    Horizon = 1:T
 
	@variable(model, x[Task,Horizon], Bin) 
	
	# minimalizacja sumy wazonych zadan
	@objective(model, Min, sum(x[j,t] * w[j] * (t+p[j]) for  j in Task, t in Horizon)) 
	
	# dokladnie jeden moment rozpoczenia j-tego zadania
	for j in Task
		@constraint(model,sum(x[j,t] for  t in 1:T-p[j]+1)==1)
	end
	
	# moment rozpoczecia j-tego zadan co najmniej jak moment gotowosci rj zadania
	for j in Task
		@constraint(model,sum(t*x[j,t] for  t in 1:T-p[j]+1)>=r[j])
	end
	# zadania nie nakladaja sie na siebie
	for t in Horizon
		@constraint(model,sum(x[j,s]  for  j in Task, s in max(1, t-p[j]+1):t)<=1)
	end
	
	status = solve(model) 
	
	if status==:Optimal
		 return status, getobjectivevalue(model), getvalue(x)
	else
		return status, nothing,nothing
	end
		
end 

# czasy wykonia j-tego zadania 
p=[ 1; 
    2;
	3;
	4;
	5]
# momenty dostepnosci j-tego zadania
r=[ 5; 
	0;
	0;
	0;
    0]
    	
# wagi j-tego zadania		
w=[ 50.0; 
	1.0;
	2.0;
	3.0;
    5.0]
        
(status, fcelu, momenty)=singleMachine(p,r,w)

if status==:Optimal
   println("funkcja celu: ", round(Int,fcelu))
   println("czas -> zadanie")
    for r in momenty.indexsets[2] 
        for r2 in momenty.indexsets[1]
            if momenty[r2,r] > 0
                print(r)
                print("\t->\t")
                println(r2)
            end
        end
    end
else
   println("Status: ", status)
end