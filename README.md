# RefreshSpike
Demonstrates updating Blazor from events in an injected service, rather than polling using timers.

Often in a JS application timers are used to poll a backend service to obtain updates. This can be improved 
using a SignalR hub to create push events that clients can listen to and await updates instead of using timers.

Blazor Server however, already _has_ a SignalR connection for the UI. So a service that generates events can
be used to raise events for changes, and the Blazor UI just needs to add an event handler to the injected event
and can update the UI without any timers or setting up a SignalR instance to achieve this.
