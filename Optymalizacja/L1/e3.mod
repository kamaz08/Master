/* 
Metody optymalizacji 
Lista 1
Kamil Sikorski 221481 
*/

set MaterialType;
set WasteType;
set ProductType;

param MaterialCost   {m in MaterialType} >= 0.0;
param MinimumMaterial{m in MaterialType} >= 0.0;
param MaximumMaterial{m in MaterialType} >= 0.0;


param ProductPrice {p in ProductType} >= 0.0;
param ProductMaterialMinimumCost{p in ProductType, m in MaterialType} >= 0.0;
param ProductMaterialMaximumCost{p in ProductType, m in MaterialType} >= 0.0;

param ProductWasteProduction{p in ProductType, w in WasteType} >= 0.0;
param WasteUtilizationCost{w in WasteType} >= 0.0;
param ProductWasteMinimumCost{p in ProductType, w in WasteType} >= 0.0;
param ProductWasteMaximumCost{p in ProductType, w in WasteType} >= 0.0;

var SumProduct{p in ProductType} >= 0.0;
var SumMaterialToProduct{m in MaterialType, p in ProductType};
var SumWaste{w in WasteType};
var SumWasteToProduct{w in WasteType, p in ProductType};
var SumMaterial{m in MaterialType};

maximize MaterialToBuy: sum{p in ProductType}(ProductPrice[p] * SumProduct[p]- sum{m in MaterialType}(MaterialCost[m] * SumMaterialToProduct[m,p]))
                        - sum{w in WasteType}(WasteUtilizationCost[w] * (SumWaste[w] - sum{p in ProductType} (SumWasteToProduct[w,p])));

s.t. MaxSumOfMaterial{m in MaterialType}: MaximumMaterial[m] >= sum{p in ProductType} (SumMaterialToProduct[m,p]);
s.t. MinSumOfMaterial{m in MaterialType}: MinimumMaterial[m] <= sum{p in ProductType} (SumMaterialToProduct[m,p]);

s.t. SumMaxWaste{w in WasteType}: SumWaste[w] = sum{p in ProductType} (SumProduct[p] * ProductWasteProduction[p,w]);
s.t. SumMaxWasteToProduct{w in WasteType}: SumWaste[w] >= sum{p in ProductType} (SumWasteToProduct[w,p]);

s.t. SumOfProduct{p in ProductType}: SumProduct[p] = sum{m in MaterialType} (SumMaterialToProduct[m,p]) + sum{w in WasteType}(SumWasteToProduct[w,p]);

s.t. MaxMaterialToProduct{p in ProductType, m in MaterialType}: SumProduct[p] * ProductMaterialMaximumCost[p,m] >= SumMaterialToProduct[m,p];
s.t. MinMaterialToProduct{p in ProductType, m in MaterialType}: SumProduct[p] * ProductMaterialMinimumCost[p,m] <= SumMaterialToProduct[m,p];

s.t. MaxWasteToProduct{p in ProductType, w in WasteType}: SumProduct[p] * ProductWasteMaximumCost[p,w] >= SumWasteToProduct[w,p];
s.t. MinWasteToProduct{p in ProductType, w in WasteType}: SumProduct[p] * ProductWasteMinimumCost[p,w] <= SumWasteToProduct[w,p];

s.t. SumMaterialTak{m in MaterialType}: SumMaterial[m] = sum{p in ProductType}(SumMaterialToProduct[m,p]);

solve;


printf '%f \n', MaterialToBuy;

for{m in MaterialType}
{
    printf "%s %f  = \n", m, SumMaterial[m];
}


for{p in ProductType}
{
    printf "%s %f  = ", p, SumProduct[p];
    for{m in MaterialType}
    {
       printf "%s %f ",m, SumMaterialToProduct[m,p];
    }
    for{w in WasteType}
    {
         printf "%s %f %f ",w,SumWaste[w], SumWasteToProduct[w,p];
    }
    printf "\n";
}
end;
