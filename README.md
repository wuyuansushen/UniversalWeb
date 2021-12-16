# UniversalWeb

### 1. Systemd service file

Path: `/etc/systemd/system/UniversalWeb.service`

```[vim]
[Unit]
Description=.NET Web App running on CentOS 7

[Service]
WorkingDirectory=/var/www/publish
ExecStart=/usr/local/bin/dotnet /var/www/publish/UniversalWeb.dll
KillSignal=SIGINT
SyslogIdentifier=dotnet-ALL
User=root
Environment=ASPNETCORE_ENVIRONMENT=Production

[Install]
WantedBy=multi-user.target
```
