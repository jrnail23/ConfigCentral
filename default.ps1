properties {
  $solution_file = ".\src\ConfigCentral.sln"
  $delivery_directory = "C:\ProgramData\ConfigCentral"
  $service_exe = 'ConfigCentral.WebApi.TopShelfHost.exe'
  $bin_folder = '.\src\ConfigCentral.WebApi.TopShelfHost\bin\Release'
  $api_home_url = 'http://localhost:5001'
}

task default -depends smokeTest

task build {
  exec { msbuild $solution_file /t:Clean /t:Build /p:Configuration=Release /v:q }
}

task deploy -depends build {
  
  $exe_path = join-path $delivery_directory $service_exe

  if (test-path $delivery_directory) {
    exec { & $exe_path uninstall }
    rd $delivery_directory -rec -force  
  }
  
  copy-item $bin_folder $delivery_directory -force -recurse -verbose
    
  exec { & $exe_path install start }
}

task smokeTest -depends deploy {
    $response = (Invoke-WebRequest -Uri $api_home_url) 

    Assert -conditionToCheck ($response.StatusCode -eq 200) `
           -failureMessage "Expected GET $api_home_url to return HTTP 200(OK), but it returned HTTP $($response.StatusCode)($($response.StatusDescription)) instead"
}