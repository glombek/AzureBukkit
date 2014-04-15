<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="AzureBukkit" generation="1" functional="0" release="0" Id="24541115-afe2-44cc-9d89-5541e3c85bfd" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="AzureBukkitGroup" generation="1" functional="0" release="0">
      <settings>
        <aCS name="BukkitServer:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AzureBukkit/AzureBukkitGroup/MapBukkitServer:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="BukkitServerInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AzureBukkit/AzureBukkitGroup/MapBukkitServerInstances" />
          </maps>
        </aCS>
      </settings>
      <maps>
        <map name="MapBukkitServer:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureBukkit/AzureBukkitGroup/BukkitServer/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapBukkitServerInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AzureBukkit/AzureBukkitGroup/BukkitServerInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="BukkitServer" generation="1" functional="0" release="0" software="C:\Users\Joe\My Box Files\Documents\GitHub\azurebukkit\AzureBukkit\AzureBukkit\csx\Debug\roles\BukkitServer" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="768" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;BukkitServer&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;BukkitServer&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AzureBukkit/AzureBukkitGroup/BukkitServerInstances" />
            <sCSPolicyUpdateDomainMoniker name="/AzureBukkit/AzureBukkitGroup/BukkitServerUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/AzureBukkit/AzureBukkitGroup/BukkitServerFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="BukkitServerUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="BukkitServerFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="BukkitServerInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
</serviceModel>