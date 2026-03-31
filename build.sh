#!/bin/bash

cd native && cargo build --release && cd ..

if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "win32" ]]; then
    LIB_EXT="dll"
    LIB_PREFIX=""
else
    LIB_EXT="so"
    LIB_PREFIX="lib"
fi

cp "native/target/release/${LIB_PREFIX}native.${LIB_EXT}" "compiler/bin/Debug/net10.0/"

dotnet run --project compiler