# TheaterSeatingAllocation
The C# console application program is used to allocate the seats in Theater based on the Theater layout provided.
Sample input:
6 6
3 5 5 3
4 6 6 4
2 8 8 2
6 6 
 
Smith 2
Jones 5
Davis 6
Wilson 100
Johnson 3
Williams 4
Brown 8
Miller 12

How to run the program?
1) Clone or Download the TheaterSeatingAllocation repoisitory into your local.
2) Open the code base using Visual Studio 2010 or above (this program was built in VS2019, .Net Framework 4.7.2).
3) Make sure "TheaterSeating" project has setup as start-up project
4) Press F5 or go to -> Debug -> Start Debugging
5) Command prompt will be appeared after successful build, provide the input as per in the Sample input section.

Sample output: 
Smith Row 1 Section 1
Jones Row 2 Section 2
Davis Row 1 Section 2
Wilson Sorry, we can't handle your party.
Johnson Row 2 Section 1
Williams Row 1 Section 1
Brown Row 4  Section 2
Miller Call to split party.

Note: When you run this program first time, it will re-store the nuget packages componenet into your local.
