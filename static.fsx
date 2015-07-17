let html = """
    <html>
        <head>
            <title>Suave Data Collector</title>
            <link href="/style.css" type="text/css" rel="stylesheet">
            <link href='http://fonts.googleapis.com/css?family=Gloria+Hallelujah' rel='stylesheet' type='text/css'>
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
        </head>
        <body ng-app="Collector" ng-cloak>
            <div ng-controller="TodoList as todo" id="container">
                
                <h1>Collector</h1>
                <div>
                    <h2>Device Motion</h2>
                    <table>
                        <tr>
                            <td>Event Supported</td>
                            <td id="dmEvent"></td>
                        </tr>
                        <tr>
                            <td>acceleration</td>
                            <td id="moAccel"></td>
                        </tr>
                        <tr>
                            <td>accelerationIncludingGravity</td>
                            <td id="moAccelGrav"></td>
                        </tr>
                        <tr>
                            <td>rotationRate</td>
                            <td id="moRotation"></td>
                        </tr>
                        <tr>
                            <td>interval</td>
                            <td id="moInterval"></td>
                        </tr>
                    </table>
                </div>
            
                <div id="data"></div>
                                
            </div>
            <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.3.15/angular.min.js"></script>
            <script src="/static/app.js"></script>
        </body>
    </html>
"""

let script = """
        var accelerations = {};
        var obs = []
        accelerations.obs = obs;

        function captureAccels(x, y, z) {
            var observation = {
                "Time": Date.now(), // milliseconds from 1 January 1970 00:00:00 UTC.
                "X": x,
                "Y": y,
                "Z": z
            }
            accelerations.obs.push(observation)
        }

    collect();
    var count = 0;
    
    function listenForAccels() {
        window.addEventListener('devicemotion', deviceMotionHandler, false);
    }
    function unListenForAccels() {
        window.removeEventListener('devicemotion', deviceMotionHandler, false);
        document.getElementById("data").innerText = JSON.stringify(accelerations)
    }
    function collect() {
        if (window.DeviceMotionEvent) {
            document.getElementById("dmEvent").innerHTML = "Cool! I can handle Device Motion. Collecting data for 20 seconds."
            setTimeout(listenForAccels, 5000);
            setTimeout(unListenForAccels, 10000);
           
      } else {
        document.getElementById("dmEvent").innerHTML = "Not supported on your device or browser.  Sorry."
      }
    }

    function deviceMotionHandler(eventData) {
        var acceleration = eventData.acceleration;
        captureAccels(acceleration.x, acceleration.y, acceleration.z);

        var info, xyz = "[X, Y, Z]";
        info = xyz.replace("X", acceleration.x).replace("Y", acceleration.y).replace("Z", acceleration.z);
        document.getElementById("moAccel").innerHTML = info;
        document.getElementById("moInterval").innerHTML = eventData.interval
    }
"""

let style = """
    html, body { margin: 0; padding: 0; width: 100%; height: 100%; }
    #container { margin: 50px; font-family: "Gloria Hallelujah", sans-serif; font-weight: 400; }
        h1 { font-weight: 700; }
        a { text-decoration: none; }
        li { margin: 20px 0 0 0; }
        input { padding: 10px; }
"""