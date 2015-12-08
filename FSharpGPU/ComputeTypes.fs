﻿namespace NovelFS.FSharpGPU


module internal ComputeArrays =
    /// Create an offset array from a supplied array and the specified offset
    let createArrayOffset offS newLength (array : ComputeArray) =
        match newLength with
        |None ->
            new ComputeArray(array.ArrayType, array.CudaPtr, array.Length, OffsetSubarray(offS), AutoGenerated)
        |Some n ->
            new ComputeArray(array.ArrayType, array.CudaPtr, n, OffsetSubarray(offS), AutoGenerated)

    /// create and fill a device array of all floats with a specific value
    let fillFloat length value =
        let mutable cudaPtr = System.IntPtr(0)
        DeviceInterop.createUninitialisedCUDADoubleArray(length, &cudaPtr) |> DeviceInterop.cudaCallWithExceptionCheck
        DeviceFloatKernels.setAllElementsToConstant(cudaPtr, 0, length, value) |> DeviceInterop.cudaCallWithExceptionCheck
        new ComputeArray(ComputeDataType.ComputeFloat, cudaPtr, length, FullArray, UserGenerated)
    /// create and fill a device array of all floats with a specific value
    let fillBool length value =
        let mutable cudaPtr = System.IntPtr(0)
        DeviceInterop.createUninitialisedCUDABoolArray(length, &cudaPtr) |> DeviceInterop.cudaCallWithExceptionCheck
        //DeviceBoolKernels.setAllElementsToConstant(cudaPtr, 0, length, 0.0) |> DeviceInterop.cudaCallWithExceptionCheck
        new ComputeArray(ComputeDataType.ComputeBool, cudaPtr, length, FullArray, UserGenerated)
    /// create and fill a device array of all floats with a specific value
    let computeArrayOfSameType length (array : ComputeArray) =
        let mutable cudaPtr = System.IntPtr(0)
        match array.ArrayType with
        |ComputeFloat -> DeviceInterop.createUninitialisedCUDADoubleArray(length, &cudaPtr) |> DeviceInterop.cudaCallWithExceptionCheck
        |ComputeBool -> DeviceInterop.createUninitialisedCUDABoolArray(length, &cudaPtr) |> DeviceInterop.cudaCallWithExceptionCheck
        |_ -> failwith "Unsupported type"
        new ComputeArray(array.ArrayType, cudaPtr, length, FullArray, UserGenerated)

module internal ComputeResults =
    let expandValueToArray length value =
        match value with
        |ResComputeFloat flt -> ResComputeArray <| ComputeArrays.fillFloat length flt
        |ResComputeBool bl -> ResComputeArray <| ComputeArrays.fillBool length bl
        |array -> array

    let length =
        function
        |ResComputeArray devArray -> devArray.Length
        |ResComputeTupleArray devArrays -> (devArrays |> List.head).Length



