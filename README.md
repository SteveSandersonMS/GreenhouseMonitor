# GreenhouseMonitor: an ASP.NET Core on WASI sample

This is a sample application showing that typical ASP.NET Core code, including nontrivial features like WebSockets (via SignalR) and gRPC-Web can work unmodified when built as a WASI-compliant `.wasm` file and run inside [wasmtime](https://github.com/bytecodealliance/wasmtime).

## How to build

First make sure you have the prerequisites:

 * To build the .NET app: [.NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0), preview 4 or later
   * Make sure that you can run `dotnet --version` and it says `7.0.100-preview.4` or later
 * To build the Svelte UI: [Node.js](https://nodejs.org/en/)
 * To run the app: [Wasmtime](https://github.com/bytecodealliance/wasmtime/releases/tag/v0.37.0) version 0.35.2 or later

Then:

 * Build the Svelte UI
   * `cd src/GreenhouseMonitor/UI`
   * `npm install`
   * `npm run build`
   * `cd ../../..`
 * Build the .NET application
   * `cd src/GreenhouseMonitor`
   * `dotnet build`

## How to run

 * `cd src/GreenhouseMonitor` (unless you're already there)
 * Now, do any one of the following:
   * `dotnet run`
   * Or, if you're using Visual Studio, press Ctrl+F5
   * Or, if you prefer to invoke wasmtime manually:
     * `wasmtime bin/Debug/net7.0/GreenhouseMonitor.wasm --tcplisten 0.0.0.0:5000`
     * Note that if you use this mechanism, it's your job to remember to do a `dotnet build` each time you make further code changes.
 * Browse to http://localhost:5000

This assumes `wasmtime` is available on your system `PATH`.

Now, if you want to send some simulated sensor data via gRPC-Web,

 * Leave the `GreenhouseMonitor` app running, and open a new terminal window
 * `cd src/SensorSimulator`
 * `dotnet run`

## How to attach a debugger

 * Start the application using this command:
   * `wasmtime bin/Debug/net7.0/GreenhouseMonitor.wasm --tcplisten 0.0.0.0:8001 --tcplisten 0.0.0.0:5000 --env DEBUGGER_FD=3`
   * Open VS Code inside the `src/GreenhouseMonitor` directory (e.g., run `code .` from there)
   * Ensure you have the [Mono Debugger](https://marketplace.visualstudio.com/items?itemName=ms-vscode.mono-debug) extension installed
   * Set a breakpoint somewhere in the .NET code
   * Go into VS Code's "Run and debug" tool and click *Attach*
   * You should see the console output `Accepted connection from client, socket fd=5` and then, shortly after, `Now listening on...`
   * At this point, if you do something to cause the breakpoint to be hit, you should see it in VS Code

## Caveats

The [`Wasi.Sdk`](https://github.com/SteveSandersonMS/dotnet-wasi-sdk) is a very early experimental preview, and many things aren't yet implemented. In particular, .NET's garbage collection is not enabled at all, so over time the memory usage will grow indefinitely until the application terminates. This happens after handling several hundred HTTP requests. This is obviously something we intend to fix soon.

Another issue you may encounter is that wasmtime's `sock_accept` support has some bugs. For example if a client disconnects ungracefully while a TCP connection is open, then the `wasmtime` process will terminate (this is particularly the case on Windows).

## About the included package binaries

Normally people don't bundle the `.nupkg` package binaries with their application sources, so you may be wondering why there are ~15 MB of binaries in the `packages/` directory in this repo. It's simply because the version of `Wasi.Sdk` that supports debugging isn't yet published to the public NuGet feed. Once the latest package builds are published, it would not be necessary to have the `packages/` directory in this repo at all.
