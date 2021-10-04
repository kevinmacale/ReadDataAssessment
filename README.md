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
