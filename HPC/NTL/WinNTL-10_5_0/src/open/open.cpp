// open.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

using namespace std;


const int N = 100;

void loop();
void helloword();
void first();
void last();
void order();
int do_lots_of_work(int x);
void scheduler();

int main()
{
	
	scheduler();
	getchar();
	return 0;
}


void loop() {
	int nthreads, tid, i;
	float a[N], b[N], c[N];

	/* Some initializations */
	for (i = 0; i < N; i++)
		a[i] = b[i] = i;


#pragma omp parallel shared(a,b,c,nthreads) private(i,tid)
	{
		tid = omp_get_thread_num();
		if (tid == 0)
		{
			nthreads = omp_get_num_threads();
			printf("Number of threads = %d\n", nthreads);
		}

		printf("Thread %d starting...\n", tid);

#pragma omp for 
		for (i = 0; i < N; i++)
		{
			c[i] = a[i] + b[i];
			printf("Thread %d: c[%d]= %f\n", tid, i, c[i]);
		}

	}  /* end of parallel section */
}

void helloword() {
	{
		int nthreads, tid;

		/* Fork a team of threads giving them their own copies of variables */
#pragma omp parallel private(nthreads, tid)
		{

			/* Obtain thread number */
			tid = omp_get_thread_num();
			printf("Hello World from thread = %d\n", tid);

			/* Only master thread does this */
			if (tid == 0)
			{
				nthreads = omp_get_num_threads();
				printf("Number of threads = %d\n", nthreads);
			}

		} /* All threads join master thread and disband */
	}
}

void first() {
	int j, i, n = 20, tid;
	int a[100];


	j = 1;
#pragma omp parallel for firstprivate(j)
	for (i = 1; i <= n; i++) {
		if (i == 1 || i == n)
			j = j + 1;
		printf("thread %d: i = %d, j = %d\n", omp_get_thread_num(), i, j);
		a[i] = a[i] + j;
	}
	printf("End of program j = %d\n", j);
}

void last() {
	int i = 0, n = 20, tid;
	int a[100];
	double x = 0.1, pi = 3.14, dx = 0.1;

#pragma omp parallel for lastprivate(x)
	for (i = 1; i <= n; i++) {
		x = sin(pi * dx * (float)i);
		printf("thread %d: i = %d, x = %lf\n", omp_get_thread_num(), i, x);
		a[i] = exp(x);
	}
	printf("i = %d, x = %lf\n", i, x);
}
void order() {
	int i, n = 20, tid;
	int myval = 10;

#pragma omp parallel for private(myval) ordered
	for (i = 1; i <= n; i++) {
		myval = do_lots_of_work(i);
#pragma omp ordered
		{
			printf("%d %d\n", i, myval);
		}
	}
}

int do_lots_of_work(int x) {
	int j = x << 1;
	printf("%d\n", x);
	for (int y = 0; y<200; y++) {
		for (int i = x; i<j; ++i) {
			++x; x += 34; x = x / 3; x = x >> 1; x = x * 7; x -= j;
		}
	}
	x++;

	if (x<0)return -x;
	return x;
}

void scheduler() {
	int i, j, k;
	double tmp;
	int Ndim = 10;
	int Pdim = 10;
	int Mdim = 10;
	double C[100];
	double A[100];
	double B[100];
	int thc[100];

	for (int t = 0; t<100; ++t) {
		thc[t] = 0;
	}

#pragma omp parallel for schedule(dynamic) private(tmp, i, j, k)
	for (i = 0; i<Ndim; i++) {
		for (j = 0; j<Mdim; j++) {

			tmp = 0.0;
			for (k = 0; k<Pdim; k++) {
				printf("thread %d, i = %d, j = %d , k = %d\n", i, j, k, omp_get_thread_num());
				++thc[omp_get_thread_num()];
				tmp += *(A + (i*Ndim + k)) *  *(B + (k*Pdim + j));
			}
			*(C + (i*Ndim + j)) = tmp;
		}
	}

	printf("\n");
	for (int t = 0; t<100; ++t) {
		if (thc[t]>0)printf("%d - %d\n", t, thc[t]);
	}
}
