# !/bin/bash

# create .txt files inside tfm_datase directory, in yolo format from unity dataset
python tools/create_yolo_dataset.py --input ./dataset --output ./tfm_dataset

# split train and validaiton, copy .txt and image from unity dataset and yolo created labels
# inside datasets directory under /train /val directories
python tools/split_dataset.py --images ./dataset --labels ./tfm_dataset --output ./datasets/tmp --val 0.3