use std::collections::HashMap;
use std::fs::File;
use std::io::prelude::*;
use std::path::Path;

fn read_file(filename: &str) -> String {
    let path = Path::new(filename);
    let display = path.display();

    // Open the path in read-only mode, returns `io::Result<File>`
    let mut file = match File::open(&path) {
        Err(why) => panic!("couldn't open {}: {}", display, why),
        Ok(file) => file,
    };

    // Read the file contents into a string, returns `io::Result<usize>`
    let mut s = String::new();
    match file.read_to_string(&mut s) {
        Err(why) => panic!("couldn't read {}: {}", display, why),
        Ok(_) => {}
    }

    // remove all instances of \r and \t
    s = s.replace("\r", "");
    s.replace("\t", "")
}

fn violate(slice: &[u32], graph_value: &Vec<u32>) -> bool {
    for i in slice.iter() {
        if graph_value.contains(i) {
            return true;
        }
    }

    return false;
}

fn main() {
    // Create a path to the desired file
    let file_content: String = read_file("../input.txt");

    let lines: Vec<&str> = file_content.split("\n").collect();

    let mut graph: HashMap<u32, Vec<u32>> = HashMap::new();
    // let mut updates: Vec<Vec<u32>> = vec![];
    let mut result: u32 = 0;

    for line in lines.iter() {
        if line.contains("|") {
            // COLLECT RULES HERE
            let rule_integers: Vec<u32> = line.split("|").map(|s| s.parse().unwrap()).collect();

            let pre = &rule_integers[0];
            let post = &rule_integers[1];

            graph.entry(*pre).or_default().push(*post);
        } else if line.contains(",") {
            // PROCESS UPDATES
            let update_integers: Vec<u32> = line.split(",").map(|s| s.parse().unwrap()).collect();

            let mut violated = false;

            for (i, update_number) in update_integers.iter().enumerate() {
                let slice = &update_integers[0..i];

                let graph_value_wrapped: Option<&Vec<u32>> = graph.get(&update_number);

                if graph_value_wrapped.is_none() {
                    continue;
                }

                let graph_value: &Vec<u32> = graph_value_wrapped.unwrap();

                if violate(slice, graph_value) {
                    violated = true;
                    break;
                }
            }

            if !violated {
                let mid = update_integers[update_integers.len() / 2];
                result += mid;
            }
        }
    }

    println!("{}", result);

    // Ok, we got all the integers, and their rules and updates
    // Sort vector of integers by the rules
}
