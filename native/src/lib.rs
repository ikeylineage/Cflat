use std::ffi::CStr;
use std::os::raw::c_char;

#[no_mangle]
pub extern "C" fn run_bytecode(input: *const c_char) {
    let c_str = unsafe { CStr::from_ptr(input) };
    let code = c_str.to_str().expect("Invalid UTF-8");
    
    println!("Rust Runtime: Executing -> {}", code);
    // Call your engine.rs logic here
}