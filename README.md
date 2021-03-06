# RefreshSpike
Demonstrates updating Blazor from events in an injected service, rather than polling using timers.

Often in a JS application timers are used to poll a backend service to obtain updates. This can be improved 
using a SignalR hub to create push events that clients can listen to and await updates instead of using timers.

Blazor Server however, already _has_ a SignalR connection for the UI. So a service that generates events can
be used to raise events for changes, and the Blazor UI just needs to add an event handler to the injected event
and can update the UI without any timers or setting up a SignalR instance to achieve this.

## Example App

Uses a Blazor Server .NET 5.0 application with a modified `WeatherForecastService` from the basic Blazor template.

As well as generating a random set of forecasts, it also runs a timer to update a random entry every five seconds. 
When this happens it also raises an event `OnForecastChanged`. Client applications using the `WeatherForecastService` 
can subscribe to this event and handle the change.

On the `/FetchData` page the row that gets updated turns green:
![RefreshSpike](https://user-images.githubusercontent.com/454494/112131279-50792e00-8bc1-11eb-9143-a216d1ec8fbf.gif)
