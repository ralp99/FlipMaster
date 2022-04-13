using UnityEngine;
using System.Collections;

public class accelerometerInput : MonoBehaviour {

	public enum XController {X,Y,Z,XY,XZ,YZ}
	public enum YController {X,Y,Z,XY,XZ,YZ}
	public enum ZController {X,Y,Z,XY,XZ,YZ}

	public bool xdir;
	public bool ydir;
	public bool zdir;

	public bool flipx;
	public bool flipy;
	public bool flipz;
	public bool wrapEdges;

	public  XController xController;
	public  YController yController;
	public  ZController zController;

	public float lowerLimit;
	public float upperLimit;

	public GameObject alert;


	float xinput;
	float yinput;
	float zinput;

	int doflipx;
	int doflipy;
	int doflipz;

	float selX;
	float selY;
	float selZ;


	void Start()
	{

		doflipx = 1;
		doflipy = 1;
		doflipz = 1;

		xinput = 0;
		yinput = 0;
		zinput = 0;


	}

		void Update () 
		{

		switch (xController) 
		{
		case XController.X:
	//		print ("X controller is X");
			selX = Input.acceleration.x;
			
			break;
			
		case XController.Y:
		//	print ("X controller is Y");
			selX = Input.acceleration.y;
			
			break;
			
		case XController.Z:
	//		print ("X controller is Z");
			selX = Input.acceleration.z;
			break;

		case XController.XY:
//			selX = Input.acceleration.x/Input.acceleration.y;
		//	selX = Input.acceleration.y/Input.acceleration.x;
//				selX = (Input.acceleration.y*Input.acceleration.x)/2;
//			selX = Input.acceleration.y*Input.acceleration.x;
		//	selX = Input.acceleration.z*Input.acceleration.x;

				selX = Input.acceleration.x+Input.acceleration.y;





			break;




		}
		


		switch (yController) 
		{
		case YController.X:
			selY = Input.acceleration.x;
			
			break;
			
		case YController.Y:
			selY = Input.acceleration.y;
			
			break;
			
		case YController.Z:
			selY = Input.acceleration.z;
			break;
			
		}

		switch (zController) 
		{
		case ZController.X:
			selZ = Input.acceleration.x;
			
			break;
			
		case ZController.Y:
			selZ = Input.acceleration.y;
			
			break;
			
		case ZController.Z:
			selZ = Input.acceleration.z;
			break;
			
		}




		if (flipx)
			doflipx = -1;
		
		if (flipy)
			doflipy = -1;
		
		if (flipz)
			doflipz = -1;
		
		
		if (xdir)
			xinput = (selX*doflipx);
		
		if (ydir)
			yinput = (selY*doflipy);
		
		if (zdir)
			zinput = (selZ*doflipz);
		





		if (wrapEdges) {

			if ((transform.position.x > lowerLimit) && (transform.position.x < upperLimit))
				transform.Translate (xinput, yinput, zinput);
			else {
				if (transform.position.x <= lowerLimit)
					transform.position = new Vector3 ((upperLimit - .02f), transform.position.y, transform.position.z);

				if (transform.position.x >= upperLimit)
						transform.position = new Vector3 ((lowerLimit + .02f), transform.position.y, transform.position.z);

			}


		}

		else transform.Translate (xinput, yinput, zinput);


		}
	
}
