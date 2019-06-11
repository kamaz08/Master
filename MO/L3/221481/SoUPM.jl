#Kamil Sikorski 221481

using DelimitedFiles;
using Dates;

using JuMP 
using GLPKMathProgInterface



const showLog = false
const showModelLog = false
const showSolution = false
const showCost = true
const saveToFile = false
const roundd = 0.000001


function PrintLog(text :: String)
    if showLog
        println("$(Dates.format(now(), "HH:MM:SS")): $text ")
    end
end

function PrintLogModel(model :: Model)
    if showModelLog
        println("$(Dates.format(now(), "HH:MM:SS"))")
        println(model)
    end
end

function PrintSolution(solution :: Dict{Int64, Array{Int,1}})
    if showSolution
        println("$(Dates.format(now(), "HH:MM:SS"))")
        println(solution)
    end
end

function PrintCost(solution:: Dict{Int64, Array{Int,1}}, data ::Matrix{Int}, path :: String, IOFILE :: IO)
    if showCost
        rmax = 0
        for k in keys(solution)
            tempmax = sum(v -> data[v,k], solution[k])
            rmax = max(rmax,tempmax)
        end
        info = "$(Dates.format(now(), "HH:MM:SS"))\tkoszt:\t$rmax\t$path\n";
        if saveToFile
            write(IOFILE, info)
        end
        print(info)
    end
end


function ReadFile(path ::String)
    PrintLog("Czytanie pliku: $path")
    data = readdlm(path, skipstart=2); 
    data = data[1:end, 2:2:end];
    data = convert(Matrix{Int}, data);
    (n,m) = size(data);
    PrintLog("Czytanie pliku:  $path  zakończone, rozmiar |J| = $n, |M| = $m")
    return data;
end

function CalculateAlfa(data ::Matrix{Int})
    result = sum(Int[minimum(data[i,:]) for i=1:size(data,1)])
    PrintLog("Alfa = $result")
    return result
end

function BinarySearch(alfa :: Int, data :: Matrix{Int})
    (n,m) = size(data)
    _low = floor(Int, alfa/m)
    _high = alfa
    _bestT = undef
    _bestCelu = undef
    _bestX = undef

    PrintLog("Binary Search: start [$_low, $_high]")

    while _low <= _high
        mid = floor(Int,(_low + _high) / 2)
        (ok, fcelu, x) = LP(mid, data)
        if ok
            _high = mid - 1
            _bestT = mid
            _bestCelu = fcelu
            _bestX = x
        else
            _low = mid + 1
        end
    end
    if _bestT == undef 
        PrintLog("Error: nie znaleziono T")
        throw(Exception)
    end 
    PrintLog("Znaleziono najlepsze T $_bestT")
    return _bestT, _bestCelu, _bestX
end

function LP(t :: Int, data :: Matrix{Int})
    (n,m) = size(data)
    PrintLog("LP dla T = $t")
	J = 1:n
    M = 1:m
    
    model = Model(solver = GLPKSolverLP())

    @variable(model, x[J, M] >= 0)

    @objective(model,Min, sum(data[j,m] * x[j,m] for j in J, m in M)) 

    for j in J, m in M
		if data[j, m] > t
			@constraint(model, x[j, m] == 0)
		end
    end
    
    for j in J 
        @constraint(model, sum(x[j,m] for m in M) == 1)
    end

    for m in M 
        @constraint(model, sum(data[j,m] * x[j,m] for j in J) <= t)
    end

    PrintLogModel(model)

	status = solve(model, suppress_warnings=true) # rozwiaz model
	
    if status==:Optimal
        PrintLog("Znaleziono optymalny dla T = $t")
		return status==:Optimal, getobjectivevalue(model), getvalue(x)[:,:]
    else
        PrintLog("Nie znaleziono optymalnego dla T = $t")
		return status==:Optimal, nothing,nothing
	end
end

function IsOne(x :: Float64)
    return x >= 1.0 - roundd
end

function IsPartial(x :: Float64)
    return x >= 0.0 + roundd && x < 1.0 - roundd
end


function BuildSolution(x :: Matrix{Float64})
    solution = Dict{Int64, Array{Int,1}}()
    jobToResolve = Dict{Int64, Array{Int,1}}()
    machinesWithPartial = Dict{Int64, Array{Int,1}}()
    integrally = [findfirst(y-> IsOne(y),x[i, :]) for i = 1: size(x,1)]
    PrintLog("Znaleziono integralne rozwiązania = $(count(i -> i != nothing, integrally))")

    for j in 1 : size(integrally)[1] 
        if(integrally[j] != nothing)
            list = get!(solution, integrally[j], [])
            push!(list, j)
        else
            machineList = get!(jobToResolve, j, findall(y-> IsPartial(y),x[j, :]))
            for machine in machineList
                jobmachines = get!(machinesWithPartial, machine, [])
                push!(jobmachines, j)
            end
        end
    end

    PrintLog("Nieintegralne zadania przydzielone do maszyn = $jobToResolve")
    PrintLog("Maszyny z nieintegralnymi zadaniami = $machinesWithPartial")

    ResolveConflicts(solution,jobToResolve,machinesWithPartial)

    PrintLog("Nieintegralne zadania przydzielone do maszyn = $jobToResolve")
    PrintLog("Maszyny z nieintegralnymi zadaniami = $machinesWithPartial")

    return solution
end

function remove!(a, item)
    deleteat!(a, findall(x->x==item, a))
end

function ResolveConflicts(
    solution :: Dict{Int64, Array{Int,1}},
    jobToResolve:: Dict{Int64, Array{Int,1}},
    machinesWithPartial :: Dict{Int64, Array{Int,1}})

    while(length(jobToResolve) > 0)
        job = undef
        key = undef
        if any(kv -> size(kv[2])[1] == 1, machinesWithPartial) 
            key = findfirst(v -> size(v)[1] == 1, machinesWithPartial)
            job = machinesWithPartial[key][1];
            PrintLog("Lisc maszynowy = $key zadanie $job")
        else
            (job,v) = first(jobToResolve)
            key = first(v)
            PrintLog("Na przemian maszyna = $key zadanie $job")
        end

        list = get!(solution, key, [])
        push!(list, job)

        for machine in jobToResolve[job]
            PrintLog("maszyny = $machine zadanie $job")
            if(size(machinesWithPartial[machine])[1] == 1)
                delete!(machinesWithPartial,machine)
            else
                remove!(machinesWithPartial[machine], job)
            end
        end
        delete!(jobToResolve, job)
    end
    return solution, jobToResolve, machinesWithPartial
end



function Solve(path :: String, IOFILE :: IO)
    data = ReadFile(path)
    alfa = CalculateAlfa(data);
    (T, fcelu, x) = BinarySearch(alfa,data)
    solution = BuildSolution(x)

    PrintSolution(solution);
    PrintCost(solution, data, path, IOFILE);
end 

function Program2()
    folders = ["instancias1a100", "instancias100a120", "instancias100a200", "instanciasde10a100", "Instanciasde1000a1100", "JobsCorre", "MaqCorre" ]

    for folder in folders
        if saveToFile
            IOFILE = open("$folder.txt", "w");
        end
        cd(folder)
        tests = readdir()

        for test in tests 
            Solve(test, IOFILE)
        end
        if saveToFile
            close(IOFILE)
        end
        cd("..")
    end
end


function Program()
    Solve("instancias1a100//1011.txt", open("a","w"))
end

Program()