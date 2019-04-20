# TL;DR

WorkspaceManager is a .NET Core application that takes a JSON configuration file to start up a list of specified external applications.

# Why?
When I log into my computer at the start of the day there is always a set of applications that I need to do my job. Typical operating systems provide means for startup scripts so that you can define this behavior on major platforms. However, the way these login scripts work aren't cross platform (AFAIK) so you would need to set this behavior up on each new computer.

I'm also quite bored.

.NET Core is cross platform, so having a single application that takes a single JSON for configuration makes this easy to set up on any OS.

# Future Plans
I'd like to add these features in the future:

- Support passing arguments to each application that must be launched.
- Have WorkspaceManager create its own services that run at login for each major OS. No additional setup required.
- Introduce a UI for editing the JSON.
  - This may be difficult since .NET Core doesn't quite have the best UI creation yet, though WPF is looking promsing.
