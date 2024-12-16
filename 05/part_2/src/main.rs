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

fn violate(slice: &[u32], graph_value: &Vec<u32>) -> i32 {
    for (index, i) in slice.iter().enumerate() {
        if graph_value.contains(i) {
            return index as i32;
        }
    }

    return -1;
}

fn rearrange(integers: &Vec<u32>, graph: &HashMap<u32, Vec<u32>>) -> Vec<u32> {
    let mut new_vector = integers.clone();

    // current_index increments, check that the slice preceding the index does not violate
    // if it violates, move current number before the violating number, then reset the index to 0

    let mut current_index: usize = 0;

    while current_index < new_vector.len() {
        let update_number = &new_vector[current_index];
        let slice = &new_vector[0..current_index];

        let graph_value_wrapped: Option<&Vec<u32>> = graph.get(&update_number);

        if graph_value_wrapped.is_none() {
            current_index += 1;
            continue;
        }

        let graph_value: &Vec<u32> = graph_value_wrapped.unwrap();

        // process where violation occurs
        let violation = violate(slice, graph_value);

        if violation > -1 {
            let violation_index: usize = violation as usize;
            // move current index element to before the violating number
            new_vector[violation_index..current_index + 1].rotate_right(1);

            current_index = 0;
        } else {
            current_index += 1;
        }
    }

    new_vector
}

fn main() {
    // Create a path to the desired file
    let file_content: String = read_file("../input.txt");

    let lines: Vec<&str> = file_content.split("\n").collect();

    let mut graph: HashMap<u32, Vec<u32>> = HashMap::new();

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

                if violate(slice, graph_value) > -1 {
                    violated = true;
                    break;
                }
            }

            if violated {
                let new_vector: Vec<u32> = rearrange(&update_integers, &graph);

                let mid = new_vector[new_vector.len() / 2];
                result += mid;
            }
        }
    }

    println!("{}", result);

    // Ok, we got all the integers, and their rules and updates
    // Sort vector of integers by the rules
}
