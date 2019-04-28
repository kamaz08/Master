using JuMP 
using GLPKMathProgInterface 

function multiMachine(p::Vector{Int}, r::Array{Array{Any,1},1}, m::Int)
    #ilosc zadan
    n= length(p)
    #maksymalny czas wykonania wszystkich zadan
    T= sum(p)
    model = Model(solver = GLPKSolverMIP())

    Task = 1:n
    Horizon = 1:T
    Machine = 1:m

    @variable(model, x[Task, Horizon, Machine], Bin) 
    @variable(model, ms>=0)
    
    #minimalizacja czasu
    @objective(model, Min, ms)

    #kazde zadanie sie rozpocznie
    for t in Task 
        @constraint(model, sum(x[t,h,m] for h in Horizon, m in Machine) == 1)
    end

    #jedno zadanie na raz na jednej maszynie
    for m in Machine, h in Horizon
        @constraint(model, sum(x[t,s,m] for t in Task, s in max(1, h-p[t]+1):h) <= 1)
    end

    #kolejnosc zadan
    for t in Task
        for t2 in r[t]
            @constraint(model, sum(x[t2, h, m] * (h + p[t2]) for h in Horizon, m in Machine ) <= sum(x[t, h, m] * h for h in Horizon, m in Machine ))
        end
    end

    #czas skonczenia wszyskich zadan
    for t in Task, h in Horizon, m in Machine
        @constraint(model, x[t, h, m] * (h + p[t]) <= ms)
    end

    status = solve(model) 
    
    if status==:Optimal
		return status, getobjectivevalue(model), getvalue(x)
	else
		return status, nothing,nothing
    end
end

m = 3
# czas zadania j
 p=[1;2;1;2;1;1;3;6;2]
# poprzedzajace zadania
    r=[[] for i=1:length(p)]
    r[1] = []
    r[2] = []
    r[3] = []
    r[4] = [1;2;3]
    r[5] = [2;3]
    r[6] = [4]
    r[7] = [4;5]
    r[8] = [5]
    r[9] = [6;7]

(status, fcelu, momenty) = multiMachine(p, r, m)    


if status==:Optimal
    println("funkcja celu: ", floor(Int, fcelu-1))
    print("T/m")
    for m in momenty.indexsets[3]
        print('\t')
        print(m)
    end


    for t in 1:floor(Int, fcelu-1)
        print("\n")
        print(t)
        for m in momenty.indexsets[3]
            print("\t")
            b = false;
            for j in momenty.indexsets[1]
                if momenty[j,t,m] != 0
                   print(j)
                   b = true;
                end
            end
            if b == false
                print("-")
            end
         end
     end
 else
    println("Status: ", status)
 end