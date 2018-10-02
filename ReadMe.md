An approach to control View during long async operations from ViewModel.

##### usage:

```csharp
using (var busyBox = new MessageBusyBox(this, "Loading started.")) {
    // any long async operation
}
```
```csharp
using (var busyBox = new CancellationBusyBox(this, "Loading started.")) {
    // any long async operation
}
```
```csharp
using (var outerBusyBox = new MessageBusyBox(this, "1st part.")) {
    // any long async operation
    using (var innerBusyBox = new MessageBusyBox(this, "2nd part.")) {
        // any long async operation
    }
}
```
```csharp
using (var busyBox = new CancellationBusyBox(this, (ControlTemplate)Application.Current.Resources["CustomCancellationBusyBoxTemplate"], "Loading started.", "Press to Cancel")) {
    // any long async operation
}
```

#### result
[![Watch the Demo](https://github.com/AliaksandrZhloba/BusyBox/blob/master/Demo.gif)]


Author: Aliaksandr Zhloba, AZhloba@tut.by
Environment: MS Visual Studio 2017
Platform: .Net Framework 4.6, C#, WPF