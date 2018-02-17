#!/bin/bash
export PACKAGEDIR=$PWD/../Packages

find ~/.nuget/packages/ -name 2.0.0-alpha2.1000 -exec rm -rf {} \;

if [ ! -d "$PACKAGEDIR" ]; then
    mkdir $PACKAGEDIR
fi

#rm $PACKAGEDIR/*
dotnet pack -p:PackageVersion=2.0.0-alpha2.1000 --include-symbols --include-source -o $PACKAGEDIR