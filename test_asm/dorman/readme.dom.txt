The tests have been edited to:
- compile with ca65
- run using a console app that provides
  - $f001 - character out register - writes to console
  - $f002 - character in status - b7 indicates character available
  - $f003 - chatacter read register reads raw byte from console

