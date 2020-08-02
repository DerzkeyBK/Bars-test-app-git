C# .NET Core ���������� ��� ��������� �������� � ��������� ��� ���������� ���� ������ PostgreSQL ��������. ���������� ������ ������� � Google Sheets � ������� ��������� ��� ������.
������������ ��������� NuGet ������:
*NPGSQL - open-source .NET ��������� ������ ��� PostgreSQL
*Newtonsoft.Json -JSON - framework ��� .NET
*Google.Apis.Sheets.v4 - ���������� ��� ������ � Sheets v4 Google API.

��� ������ � ����������� ���������� ��������� ���� �������� appsettings.json
������ appsettings.json
```
{
  "dbconnections": [
    {
      "db_name": "��� ������ �������",
      "connection_string": "������ ����������� � ��",
      "max_size": ������ ������ ����� � ���������
    },
    {
      "db_name": "��� ������ �������",
      "connection_string": "������ ����������� � ��",
      "max_size": ������ ������ ����� � ���������
    },
  ],
  "googleconnection": {
    "clientID": "Id ������ ���",
    "clientSecret": "��������� ���� ���",
    "sheetId": "���� ������� ������ �� docs.google.com/spreadsheets/d/==>���� �������<==/edit#gid=1651728445"
  },
  "timeoffset": { ��� ������ ����������
    "days": 1,
    "hours": 1,
    "minutes": 1,
    "seconds": 1
  }
}
```
��� ���� ������ ���� � ������ ��� ���������� �� �����������, ���� �� ������ ������� ���� ��� �� ������ https://developers.google.com/sheets/api/quickstart/dotnet