bool changed;
do{
	changed=false;
	for(int p=0;p<n-1;p++)
	for(int q=p+1;q<n;q++){
		double apq=A[p,q], app=A[p,p], aqq=A[q,q];
		double theta=0.5*Atan2(2*apq,aqq-app);
		double c=Cos(theta),s=Sin(theta);
		double new_app=c*c*app-2*s*c*apq+s*s*aqq;
		double new_aqq=s*s*app+2*s*c*apq+c*c*aqq;
		if(new_app!=app || new_aqq!=aqq) // do rotation
			{
			changed=true;
			timesJ(A,p,q, theta); // A←A*J 
			Jtimes(A,p,q,-theta); // A←JT*A 
			timesJ(V,p,q, theta); // V←V*J
			}
	}
}while(changed);
