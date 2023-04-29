#!/bin/sh
dotnet restore ./Shared/projects/Contracts/PmSim.Shared.Contracts
dotnet restore ./Shared/projects/GameEngine/PmSim.Shared.GameEngine
dotnet pack ./Shared/projects/Contracts/PmSim.Shared.Contracts -o ./Nugets
dotnet pack ./Shared/projects/GameEngine/PmSim.Shared.GameEngine -o ./Nugets

dotnet restore -s ./Nugets -s https://api.nuget.org/v3/index.json
