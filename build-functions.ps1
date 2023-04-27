$solutionName = "CM.PeriodicWordEmitter"
$path_base_dir = resolve-path .\
$path_source_dir = "$path_base_dir\src"
$path_solution_file = "$path_source_dir\$solutionName.sln"
$path_library_project = "$path_source_dir\CM.PeriodicWordEmitter"
$path_build_dir = "$path_base_dir\build"
$path_test_dir = "$path_base_dir\test"
$verbosity = "minimal" # "normal"
$buildConfig = "Release"

function Clean {
	rd $path_build_dir -recurse -force  -ErrorAction Ignore
	md $path_build_dir > $null

	rd $path_test_dir -recurse -force  -ErrorAction Ignore
	md $path_test_dir > $null

	Write-Host "Cleaning configuration '$buildConfig' of solution '$solutionName'"
	& dotnet clean $path_source_dir\$solutionName.sln --nologo --configuration $buildConfig --verbosity $verbosity
}

function Prepare {
	& dotnet restore $path_source_dir\$solutionName.sln --nologo --interactive --verbosity $verbosity
}

function Compile {
	Write-Host "Compiling project build configuration '$buildConfig'"
	& dotnet build $path_source_dir\$solutionName.sln --nologo --no-restore --verbosity $verbosity --configuration $buildConfig --no-incremental
}

function RunTests{
	Write-Host "Running unit tests for '$path_solution_file'"
	Write-Host "Test results going to '$path_test_dir'"
	& dotnet test --nologo --verbosity $verbosity --logger:trx --results-directory $path_test_dir --no-build --no-restore --configuration $buildConfig --collect:"Code Coverage" $path_solution_file
}

function MakePackage{
	Write-Output "Creating library nuget package"
	& dotnet pack $path_library_project --nologo --no-restore --no-build --configuration $buildConfig --output $path_build_dir --verbosity $verbosity
}

function DevBuild{
	Write-Host "Running Dev Build"
	Clean
	Prepare
	Compile
	RunTests
}

function CIBuild{
	Write-Host "Running CI Build"
	Clean
	Prepare
	Compile
	RunTests
	MakePackage
}
