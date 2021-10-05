# DTN.ReadDataAssessment

A program that reads lightning data as a stream from standard input (one lightning strike per line as a JSON object, and matches that data against a source of assets (also in JSON format) to produce an alert.

An example 'strike' coming off of the exchange looks like this:
```
{
    "flashType": 1,
    "strikeTime": 1386285909025,
    "latitude": 33.5524951,
    "longitude": -94.5822016,
    "peakAmps": 15815,
    "reserved": "000",
    "icHeight": 8940,
    "receivedTime": 1386285919187,
    "numberOfSensors": 17,
    "multiplicity": 1
}
```

## WHERE
flashType=(0='cloud to ground', 1='cloud to cloud', 9='heartbeat')

strikeTime=the number of milliseconds since January 1, 1970, 00:00:00 GMT

## NOTE
A 'heartbeat' flashType is not a lightning strike. It is used internally by the software to maintain connection.

An example of an 'asset' is as follows:
```
{
    "assetName":"Dante Street",
    "quadKey":"023112133033",
    "assetOwner":"6720"
}
```

For each strike received, it should simply print to the console the following message:

## Lightning Alert for assetOwner:assetName

When owner already alerted, we can never see it again on the messages unless we terminate the application

CONFIGURATION
-------------
 
 * Run the application

 * Choose from the menu
      - y/Y for reloading the Data and printing the alert messages
      - n/N for terminating the application

TIME COMPLEXITY
---------------
Time complexity is commonly estimated by counting the number of elementary operations performed by the algorithm, supposing that each elementary operation takes a fixed amount of time to perform. Thus, the amount of time taken and the number of elementary operations performed by the algorithm are taken to differ by at most a constant factor.

To this kind of process and algorithm, the time complexity for a particular asset will take an average of:
![image](https://user-images.githubusercontent.com/33210942/135962593-e4a270df-f7ab-4f79-9f0f-cf882326157d.png)

PRODUCTION PERFORMANCE ISSUE
---------------------------------------

There are several factors that might cause performance issues like the ff:
* Reflections
* Not enough indexing
* Not having an lookup values

To speed up the process it would be better to check and refactor all those possible cause of a slow app performance.




