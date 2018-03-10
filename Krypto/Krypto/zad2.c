#include <sys/times.h>
#include <math.h>
#include <stdio.h>

int tab[31];
int main(){
    struct tms time;
    srandom(times(&time));
    int count = 0;
    
    for(int i =0; i<31; i++)
        tab[i] = random();
    for(int i =31; i<1000; i++){
        int res =((unsigned int) (2*tab[(i-31)%31] + 2*tab[(i-3)%31])) >> 1;
        int res0 = random();
        printf("%i %i\n",res, res0);
        if(res0 == res) count++;
        tab[i%31]=res0;
    }
        
    printf("%f", ((double) count / (double)(1000-31)))  ;  
    return 0;
}

//https://www.codechef.com/ide