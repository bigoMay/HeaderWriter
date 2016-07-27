# HeaderWriter
Tool to write text headers in a set of files given a filter

Invoke from the command line with 3 arguments: 

HeaderWriter.exe [path to file with header text] [path to folder that contains the files where the header should be written] [filter]

Example: 

HeaderWriter.exe  c:/headertext.txt  c:/destfolder  *.cs"

Note: For safety reasons, it is recommended to make a copy of the files before running the program. A backup of the original files will also be provided by the program.
