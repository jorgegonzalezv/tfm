#!/bin/bash

LIBPATH=lib/yolov5-master
NAME=09_07_tfm_seadronesee  # model to use
EPOCHS=100
PATIENCE=100
WEIGHTS=$LIBPATH/runs/train/09_07_tfm/weights/best.pt


python $LIBPATH/train.py --weights $WEIGHTS \ 
    --data datasets/seadronesee/data_tfm.yaml \ 
    --name $NAME \ 
    --freeze 10 \ 
    --optimizer Adam \ 
    --patience $PATIENCE \ 
    --epochs $EPOCHS \ 
    --hyp $LIBPATH/data/hyps/hyp.VOC.yaml