/*Monika Kolbuch 221519*/


/*input data*/

set Products;
set Materials;
param price{p in Products};
param minimum_percent{m in Materials, p in Products};
param maximum_percent{m in Materials, p in Products};
param minimum_order{m in Materials};
param maximum_order{m in Materials};
param materials_cost{m in Materials};
param waste_factor{m in Materials, p in Products};
param destroy_cost{m in Materials, p in Products};

/*variable*/

var get_materials{m in Materials, p in Products} >= 0.0;
var get_waste{m in Materials, p in Products} >= 0.0;

/*objective function*/

maximize profit :
       sum{p in Products, m in Materials}((get_materials[m, p] * (1.0 - waste_factor[m, p]) + get_waste[m, p]) * price[p])
      -sum{p in Products, m in Materials}(get_materials[m, p] * materials_cost[m])
      -sum{m in Materials}((get_materials[m, 'A'] * waste_factor[m, 'A'] - get_waste[m, 'C']) * destroy_cost[m, 'A']) 
      -sum{m in Materials}((get_materials[m, 'B'] * waste_factor[m, 'B'] - get_waste[m, 'D']) * destroy_cost[m, 'B']); 

/*constraints*/

subject to min_order {m in Materials} :
    sum{p in Products}(get_materials[m, p]) >= minimum_order[m];

subject to max_order {m in Materials} :
    sum{p in Products}(get_materials[m, p]) <= maximum_order[m];

subject to waste_for_A {m in Materials} :
    get_waste[m, 'A'] = 0;

subject to waste_for_B {m in Materials} :
    get_waste[m, 'B'] = 0;

subject to waste_for_C{m in Materials} :
    get_waste[m, 'C'] <= get_materials[m, 'A'] * waste_factor[m, 'A'];

subject to waste_for_D{m in Materials} :
    get_waste[m, 'D'] <= get_materials[m, 'B'] * waste_factor[m, 'B'];

subject to percent_minimal{m in Materials, p in Products} :
    get_materials[m, p] >= sum{mm in Materials}(get_materials[mm, p] + get_waste[mm, p]) * minimum_percent[m, p];

subject to percent_maximal{m in Materials, p in Products} :
    get_materials[m, p] <= sum{mm in Materials}(get_materials[mm, p] + get_waste[mm, p]) * maximum_percent[m, p];

solve;

/*displaying results*/

printf "\n";
for {m in Materials, p in Products: get_materials[m ,p] > 0.0}
    printf"Materials %s for product %s %g kg\n", m, p, get_materials[m, p];

printf "\n";
for {m in Materials, p in Products: get_waste[m ,p] > 0.0}
    printf"Wastes %s for product %s %g kg\n", m, p, get_waste[m, p];
printf "\n";

for {m in Materials: (get_materials[m, 'A'] * waste_factor[m, 'A'] - get_waste[m, 'C']) > 0.0}
    printf  "Destroy from %s A %g kg\n", m, (get_materials[m, 'A'] * waste_factor[m, 'A'] - get_waste[m, 'C']);

for {m in Materials: (get_materials[m, 'B'] * waste_factor[m, 'B'] - get_waste[m, 'D']) > 0.0}
    printf  "Destroy from %s B %g kg\n", m, (get_materials[m, 'B'] * waste_factor[m, 'B'] - get_waste[m, 'D']);

printf "\n";

/*data section*/

data;

set Products := A B C D;
set Materials := M1 M2 M3;

param price :=
                A 3.0
                B 2.5
                C 0.6
                D 0.5;

param minimum_percent :       A   B   C   D   :=
                        M1  0.2 0.1 0.2 0.0
                        M2  0.4 0.0 0.0 0.3
                        M3  0.0 0.0 0.0 0.0;

param maximum_percent :       A   B   C   D   :=
                        M1  1.0 1.0 0.2 0.0
                        M2  1.0 1.0 0.0 0.3
                        M3  0.1 0.3 0.0 0.0;

param minimum_order :=
                        M1 2000.0
                        M2 3000.0
                        M3 4000.0;

param maximum_order :=
                        M1 6000.0
                        M2 5000.0
                        M3 7000.0;

param materials_cost :=
                        M1 2.1
                        M2 1.6
                        M3 1.0;

param waste_factor :      	 A    B    C    D   :=
                  	  M1   0.1  0.2  0.0  0.0
                   	  M2   0.2  0.2  0.0  0.0
                    	  M3   0.4  0.5  0.0  0.0;

param destroy_cost :       	A    B     C    D     :=
                           M1   0.1  0.05  0.0  0.0
                           M2   0.1  0.05  0.0  0.0
                           M3   0.2  0.4   0.0  0.0;

end;