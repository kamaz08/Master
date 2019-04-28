#Kamil Sikorski 221481

using JuMP 
using GLPKMathProgInterface # pakiet GLPK

struct P
	a::Int # 7
	b::Int # 5
    c::Int # 3
    d::Int # reszta
end	

Mozliwosc = [];

#dane
r = 22;
zapo7 = 110
zapo5 = 120;
zapo3 = 80;

for a = 0:floor(r/7), b = 0:floor(r/5), c = 0:floor(r/3)
   odpad = r - (a*7+b*5+c*3);
   if odpad >= 0 
    push!(Mozliwosc, P(a,b,c,odpad))
   end
end

println(Mozliwosc)

model = Model(solver = GLPKSolverMIP())
@variable(model, x[Mozliwosc] >= 0, Int) 

@objective(model, Min, sum(a.d * x[a] for a in Mozliwosc) + (sum(x[a] * a.a for a in Mozliwosc) - zapo7)*7 + (sum(x[a] * a.b for a in Mozliwosc) - zapo5) * 5 + (sum(x[a] * a.c for a in Mozliwosc) - zapo3) * 3  )

@constraint(model, sum(x[a] * a.a for a in Mozliwosc) >= zapo7)
@constraint(model, sum(x[a] * a.b for a in Mozliwosc) >= zapo5)
@constraint(model, sum(x[a] * a.c for a in Mozliwosc) >= zapo3)


print(model) # print the instance of problem

status = solve(model, suppress_warnings=true) # solve model

if status==:Optimal
  println("ilosc odpadkow: ", getobjectivevalue(model))
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