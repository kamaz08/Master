/* 
Metody optymalizacji 
Lista 1
Kamil Sikorski 221481 
*/

set VehicleType;
set Town;

param TownDistance          {i in Town, j in Town} >= 0.0;
param VehicleRate           {v in VehicleType} >= 0.0;
param VehicleDeficiencyRate {v in VehicleType} >= 0;
param VehicleReplacement    {v in VehicleType, v2 in VehicleType};
param Deficiency            {i in Town, v in VehicleType} >= 0;
param Overload              {i in Town, v in VehicleType} >= 0;


var VehicleTownReplacement               {i in Town, j in Town, v in VehicleType} >= 0;
var VehicleTownReplacementCost           {i in Town, j in Town} >= 0.0;
var VehicleTownReplacementDeficiencyCost {i in Town} >= 0.0;
var VehicleDeficiency                    {i in Town, v in VehicleType} >= 0;

minimize VehicleTownReplacementAllCostMin:      sum{i in Town} ( sum{j in Town} (VehicleTownReplacementCost[j,i]) + VehicleTownReplacementDeficiencyCost[i]);

s.t. MaxFromTown      {i in Town, v in VehicleType }: 
    sum{j in Town}(VehicleTownReplacement[i,j,v]) <= Overload[i,v];

s.t. ReplaceCost      {i in Town, j in Town}:
    VehicleTownReplacementCost[i,j] = sum{v in VehicleType}(VehicleTownReplacement[i,j,v] * TownDistance[i,j] * VehicleRate[v]);

s.t. VehicleTownDeficiency{i in Town, v in VehicleType}: VehicleDeficiency[i,v] = 
    Deficiency[i,v] -(sum{j in Town, v2 in VehicleType: v == v2}(VehicleTownReplacement[j,i,v2]) +
        sum{v2 in VehicleType: v != v2 && VehicleReplacement[v,v2] == 1}(sum{j in Town}(VehicleTownReplacement[j,i,v2]) - Deficiency[i,v2] 
        + (Overload[i,v2] - sum{j in Town}(VehicleTownReplacement[i,j,v2]))));

s.t. CostDeficiency {i in Town}: VehicleTownReplacementDeficiencyCost[i] = 
    sum{v in VehicleType} (VehicleDeficiencyRate[v] * VehicleDeficiency[i,v]);

solve;

printf '\n';
for{i in Town}{
		for{j in Town}{
			for{v in VehicleType}{
                for{{0}:VehicleTownReplacement[j,i,v]>0}{
                    printf "%s <- %s = %f (%s)\n" ,i , j, VehicleTownReplacement[j,i,v], v;
                }
			}
		}
	}
    printf '\n';
for{i in Town}{
    for{v in VehicleType}{
         printf "%s %s %d\n",i, v, VehicleDeficiency[i,v] * VehicleDeficiencyRate[v];    
    }
}
printf "\nKoszt calkowity = %f\n", VehicleTownReplacementAllCostMin;

end;
